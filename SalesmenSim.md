# Salesmen Simulator — Design Summary

## Owner

The player. Persists across the whole game (and eventually across multiple stores, though that's a future phase).

- **Cash balance** — the single source of truth for money; all store revenue/expenses flow through here
- **People Skill** — determines how close to a customer's true budget you need to land for an offer to be accepted (higher skill = more forgiving margin)
- **Technical Skill** — affects car buying; flagged for a future "true vs. perceived condition" mechanic where skill narrows the gap between what you see and what you actually get

In Phase 1, Owner _is_ the salesman and mechanic — no employees needed yet.

## Store

The location. Single store for now, though the Owner→Stores relationship is built to extend later.

- **Tier** — a paid upgrade (capital investment). Gates:
  - Car catalog access (which models you can buy)
  - Buy price multiplier
  - Customer budget range (richer customers at higher tiers)
  - **Rating's hard ceiling** (Tier 1 → 60/100, Tier 2 → 70, Tier 3 → 80, Tier 4 → 100; Tier 5 unbuilt for now, future prestige bonus)
- **Rating** — earned, 0–100 internally, displayed as 1–5 stars. No floor (bad service always hurts, regardless of Tier). Drives:
  - Daily customer count (+1 customer per 10 raw Rating points)
  - Customer negotiation leniency (via Owner's People Skill interacting with it — the two stats are in tension)
- **Size** — lot capacity. Drives:
  - Max cars held
  - Restock purchase limit (= empty slots, i.e. Size minus current inventory)
  - Restock batch size shown
  - Restock reroll allowance
- **Cars list** — current inventory
- **Employees** (Manager/Salesman/Mechanic) — future phase, only needed once multi-store makes Owner unable to be everywhere

## Car

Comes from a catalog (models with base prices, Tier-gated).

- **Model** — from catalog, determines base price and Tier availability
- **Condition** (D–S) — rolled at restock, fixed once created. Acts as:
  - A flat price multiplier off base price
  - A one-time post-sale risk roll (worse condition = higher chance of a complaint that dings Rating)
- Flagged for later: true vs. perceived condition (Technical Skill mitigates this risk at buy-time)

## Customer

One at a time, sequential — no multi-customer floor simulation.

- **Difficulty** (Easy/Medium/Hard enum) — defines an _envelope_, not a fixed pair. Within that envelope, each customer independently rolls:
  - **Budget range width** (visible to Owner) — Easy → narrow, Hard → wide
  - **Offer attempt count** — Easy → many, Hard → few
- **True budget** — hidden, somewhere inside the visible range; this is what offers are actually checked against
- **Preferred car type** — binary match (you have it or you don't). No match → budget range widens _at the bottom only_ (ceiling unchanged, floor drops — same best-case, worse likely-case)
- Every customer gets an attempt; no declining service (for now)

## The Sale Loop (per customer)

1. Customer's range, difficulty, and offer count are visible
2. Owner names a price
3. Checked against hidden true budget; People Skill affects how much slack is allowed
4. Accept → sale closes, no penalty regardless of offers used
5. Reject → offer count ticks down, try again
6. Run out of offers with no sale → Rating hit

## The Day Loop

1. **Restock phase** — batch of cars shown (size scales with Store Size), Owner can reroll (escalating fee per reroll, reroll cap scales with Size), buy up to current empty slot count
2. **Customer phase** — number of customers scales with Rating; each goes through the sale loop
3. **End-of-day summary** — cars sold, profit, cash balance, Rating change, customers lost
4. Back to restock

---

**The throughline:** a small number of stats (Tier, Rating, Size, Difficulty) each quietly drive multiple downstream systems rather than every mechanic needing its own dedicated variable — that's been the consistent design pattern across all of this.
