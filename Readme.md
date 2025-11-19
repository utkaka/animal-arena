Game development is extremely sensitive to speed. While we are pondering architecture, our competitors are already checking their metrics on spaghetti code. So our main task is to deliver new functionality to game designers and the rest of production as quickly as possible, staying just a few steps ahead of them with our engineering explorations.

This work does not claim to be a complete architecture; it is only a two-day prototype built on some foundation that will be expanded and reinforced later. Refactoring is better done iteratively, taking into account current issues and future plans.

Possible next steps in this project:
- Further separating the domain layers.
- Integrating Addressables.
- Integrating UniTask for IO; it can also simplify local logic (state machines can be simplified by composing sequences of actions).
- Integrating UniRx as the UI grows: it will simplify reactivity and make interactive UI elements easier to work with.