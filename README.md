# 🐦 Flappy Bird NEAT AI

## 📌 Overview
This project is a Unity implementation of **Flappy Bird** where multiple birds learn to play the game using a **Neural Network + Genetic Algorithm (NEAT-inspired)**.  
Birds evolve over generations, starting from random flapping to gradually learning strategies that keep them alive longer.

---

## 🎮 Features
- Classic Flappy Bird gameplay built in **Unity**  
- AI birds controlled by neural networks  
- Genetic Algorithm for evolving birds across generations  
- Adjustable parameters: population size, mutation rate, hidden neurons, etc.  
- UI that shows:
  - Current **Score**
  - Current **Generation**
  - Number of **Birds Alive**

---

## 🛠️ Tech Stack
- **Unity** (Game Engine)  
- **C#** (Gameplay & AI logic)  

---

## 📂 Project Structure
FlappyBirdNEAT/

├── Assets/ # Scripts, Prefabs, Sprites, Scenes

├── Packages/ # Unity package manifest

├── ProjectSettings/ # Unity project settings

└── README.md # Project documentation


> ⚠️ Note: `Library/`, `Temp/`, and other auto-generated Unity folders are excluded (see `.gitignore`).

---

## 🚀 Setup & Installation

1. **Clone this repository**
   ```bash
   git clone https://github.com/ahmadkhanraj01/FlappyBird-NEAT.git
   cd FlappyBird-NEAT  
1. Open **Unity Hub**  
2. **Add project** from this folder  
3. Make sure to use Unity version **3.13.1 or later**  

### Play the Game
1. Open the scene:
   Assets/Scenes/MainScene.unity
2. Press ▶️ **Play** in Unity Editor  

---

## 🎓 How the AI Works
Each bird is controlled by a simple **Neural Network (4-6-1)**:

### 🔹 Inputs (4):
- Bird Y position  
- Distance to next pipe (X)  
- Gap center relative Y distance  
- Bird’s vertical velocity  

   <img width="143" height="256" alt="NEAR bird Inputs" src="https://github.com/user-attachments/assets/6efb5d1e-ca7a-46eb-baa9-cb1faf7698c1" />

### 🔹 Hidden Layer
- 6 neurons

### 🔹 Output (1)
- Whether to **flap** or not  

### 🔹 Fitness Function
1. **This is Fitness Function**
   ``` C sharp
   fitness = pipesPassed * 1000 + timeAlive  

### 🔹 Neural Network
<svg xmlns="http://www.w3.org/2000/svg" width="1536" height="695" style="cursor: move;"><g transform="translate(-121.99736512533059,-45.15870062040733) scale(1.319507910772895)"><path class="link" marker-end="" d="M608,277.5, 788,237.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,277.5, 788,277.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,277.5, 788,317.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,277.5, 788,357.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,277.5, 788,397.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,277.5, 788,437.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,317.5, 788,237.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,317.5, 788,277.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,317.5, 788,317.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,317.5, 788,357.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,317.5, 788,397.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,317.5, 788,437.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,357.5, 788,237.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,357.5, 788,277.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,357.5, 788,317.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,357.5, 788,357.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,357.5, 788,397.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,357.5, 788,437.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,397.5, 788,237.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,397.5, 788,277.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,397.5, 788,317.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,397.5, 788,357.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,397.5, 788,397.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M608,397.5, 788,437.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M788,237.5, 968,337.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M788,277.5, 968,337.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M788,317.5, 968,337.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M788,357.5, 968,337.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M788,397.5, 968,337.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><path class="link" marker-end="" d="M788,437.5, 968,337.5" style="stroke-width: 0.5; stroke-opacity: 1; stroke: rgb(80, 80, 80); fill: none;"></path><circle r="10" class="node" id="0_0" cx="608" cy="277.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="0_1" cx="608" cy="317.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="0_2" cx="608" cy="357.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="0_3" cx="608" cy="397.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="1_0" cx="788" cy="237.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="1_1" cx="788" cy="277.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="1_2" cx="788" cy="317.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="1_3" cx="788" cy="357.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="1_4" cx="788" cy="397.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="1_5" cx="788" cy="437.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><circle r="10" class="node" id="2_0" cx="968" cy="337.5" style="fill: rgb(255, 255, 255); stroke: rgb(51, 51, 51);"></circle><text class="text" dy=".35em" x="573" y="477.5" style="font-size: 12px;">Input Layer ∈ ℝ⁴</text><text class="text" dy=".35em" x="753" y="477.5" style="font-size: 12px;">Hidden Layer ∈ ℝ⁶</text><text class="text" dy=".35em" x="933" y="477.5" style="font-size: 12px;">Output Layer ∈ ℝ¹</text></g><defs><marker id="arrow" viewBox="0 -5 10 10" markerWidth="7" markerHeight="7" orient="auto" refX="40"><path d="M0,-5L10,0L0,5" style="stroke: rgb(80, 80, 80); fill: none;"></path></marker></defs></svg>![nn](https://github.com/user-attachments/assets/47dd0965-2fed-4352-973c-ba51e419e7d3)
 
## 📸 Screenshots



### AI Training (Multiple Birds)
<img width="1920" height="1080" alt="Screenshot (44)" src="https://github.com/user-attachments/assets/863b3a07-d610-4e71-a115-83e2b6bb8652" />



---

## 📊 Results
- **Generation 1:** Birds die instantly at the first pipe.  
- **Generation 10+:** Some survive a few pipes.  
- **Generation 50+:** Birds develop consistent strategies.  

---

## 📝 Future Improvements
- Implement **full NEAT algorithm** (dynamic topology).  
- Add **Replay Best Bird** mode from saved brain (`bestBrain.json`).  
- Add support for **saving/loading entire populations**.  
- Visualize **neural network weights** in real time.  

---

## 🤝 Contributing
Contributions are welcome!  
Feel free to fork this project, open issues, or submit pull requests.  

---

## 📜 License
This project is licensed under the **MIT License**.  
You are free to use and modify with attribution.  

