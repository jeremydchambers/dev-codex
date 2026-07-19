# Azure Service Bus — Reliability

## Series

1. [Why Messaging](../azure-service-bus--why-messaging/)
2. [Queues vs Topics](../azure-service-bus--queues-topics/)
3. **Reliability** (this node)
4. [Sessions & Competing Consumers](../azure-service-bus--sessions/)
5. [Practice](../azure-service-bus--practice/)

**Prev:** [← Queues vs Topics](../azure-service-bus--queues-topics/) ·
**Next:** [Sessions & Competing Consumers →](../azure-service-bus--sessions/)

## What it demonstrates

Core delivery semantics for Service Bus: at-least-once delivery, locks, retries,
dead-letter queues (DLQ), and why consumers must be idempotent.

## Key ideas

### At-least-once delivery

Service Bus will deliver a message **at least once**. After a crash, timeout, or
abandon, another delivery can occur. Design for duplicates — do **not** assume
exactly-once processing unless you add your own idempotency.

### PeekLock vs ReceiveAndDelete

| Mode | Behavior | Prefer when… |
|------|----------|--------------|
| **PeekLock** (recommended almost always) | Broker locks the message; complete on success; abandon/unlock on failure | Processing can fail and must be retried safely |
| **ReceiveAndDelete** | Message is removed as soon as it is received | Losing a message is acceptable and work is tiny / instantly durable elsewhere |

If the process dies mid-work under ReceiveAndDelete, the message is already gone.

### Retries and back-off

Treat **broker redelivery** and **app-level retries** as different levers.

**Broker redelivery (automatic):** When a PeekLock holder abandons (or the lock
expires), Service Bus makes the message available again and increments delivery
count. You do **not** choose the delay between those deliveries in application
code — the broker owns redelivery. After **MaxDeliveryCount**, the message goes
to the DLQ.

**App-level back-off (your outbound calls):** While you *hold* the lock, you may
retry transient failures against a dependency (HTTP 429/503, brief SQL blips):

1. Use a short, capped exponential back-off (e.g. 200ms → 400ms → 800ms) with a
   small max attempts **inside** the current delivery.
2. Prefer cancelling / failing fast if back-off would outlast the remaining lock
   duration — then abandon (or let the lock expire) so another delivery can retry
   later, rather than holding a message while sleeping for minutes.
3. Do **not** infinite-loop retries in the consumer; poison traffic belongs on
   the DLQ after MaxDeliveryCount.
4. For permanent failures (bad payload, business rule reject), prefer fail/abandon
   (or explicit dead-letter) over retrying forever.

```mermaid
flowchart TD
  D[Delivery under PeekLock] --> W[Do work / call downstream]
  W -->|success| C[Complete]
  W -->|transient fault| B{Attempts left and lock time OK?}
  B -->|yes| S[Short back-off] --> W
  B -->|no| A[Abandon → broker redelivery or DLQ]
  W -->|permanent fault| A
```

Pair both layers with idempotency: broker redelivery **will** happen; app
back-off only reduces how often you hammer a sick dependency on *this* attempt.

### Dead-letter queue (DLQ)

After **MaxDeliveryCount** failed deliveries (or an explicit dead-letter), the
message moves to the entity’s DLQ. Use the DLQ for poison messages and ops triage —
not as the happy path.

```mermaid
flowchart TD
  M[Message on queue] --> L[PeekLock delivery]
  L -->|Complete| D[Done]
  L -->|Abandon / lock expired| R{Under MaxDeliveryCount?}
  R -->|yes| L
  R -->|no| Q[Dead-letter queue]
```

### Idempotency (checklist)

Because of at-least-once:

1. Prefer an **idempotency key** (e.g. `MessageId` / business `orderId`).
2. Record “already processed” in durable storage before side effects — or make
   side effects themselves idempotent.
3. Treat redelivery as normal, not exceptional.

(Practice comments this checklist; it does not implement a store.)

### Practice host aside

Azure Functions Service Bus triggers use **lock-based** (PeekLock-style) receive.
Throwing from the function abandons the message so it can retry and eventually DLQ.
See [Practice](../azure-service-bus--practice/).

## How to use

No code in this node. Continue to
[Sessions & Competing Consumers](../azure-service-bus--sessions/) for ordering and scale-out.
