# Endless Runner Bean
<img width="654" height="417" alt="image" src="https://github.com/user-attachments/assets/9c0221ac-49fe-457e-9c6b-3470d042d854" />

## Core Project Summary
* **Movement & Camera:** Rigidbody-based player movement in `FixedUpdate` paired with an offset-tracking camera script.
* **Procedural Track Generation:** `GroundSpawner` instantiates an initial 15 ground tiles, while `GroundTile` dynamically spawns new tiles ahead and cleans up old ones using a Z-coordinate threshold.
* **Obstacle System:** Randomly selects designated child spawn points on each tile prefab to instantiate obstacle blocks.
* **Coin Generation & Alignment:** Spawns coins on each tile using random collider point generation, adjusted with Z-axis offsets to prevent clustering in the middle and overlap with obstacle blocks. Coins are parented to the tile so they automatically destroy when the tile despawns.
* **Scoring & Coin Pickup:** `Coin` script uses trigger collisions to detect the player tagged as `"Player"`, which calls `GameManager.inst.IncrementScore()` and destroys the coin.
* **Game Management:** A singleton `GameManager` script manages score tracking and UI text updates.
