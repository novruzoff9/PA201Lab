const timer = document.getElementById("timer");
const startBtn = document.getElementById("start");
const stopBtn = document.getElementById("stop");
const pauseBtn = document.getElementById("pause");
const resetBtn = document.getElementById("reset");
const lapBtn = document.getElementById("lap");

const logs = document.getElementById("logs")

timer.innerText = "00:00:00";

let hour = 0;
let minute = 0;
let second = 0;
let millisecond = 0;

let timerInterval;

startBtn.onclick = function () {
  timerInterval = setInterval(() => {
    millisecond++;
    if (millisecond == 10) {
      second++;
      millisecond = 0;
    }
    if (second == 60) {
      minute++;
      second = 0;
    }
    timer.innerText = `${minute.toString().padStart(2, "0")}:${second
      .toString()
      .padStart(2, "0")}:${millisecond.toString().padStart(2, "0")}`;
  }, 100);
};

pauseBtn.onclick = function () {
  clearInterval(timerInterval);
  startBtn.innerText = "Resume";
};

stopBtn.onclick = function () {
  hour = 0;
  minute = 0;
  second = 0;
  millisecond = 0;
  clearInterval(timerInterval);
  timer.innerText = `${minute.toString().padStart(2, "0")}:${second
    .toString()
    .padStart(2, "0")}:${millisecond.toString().padStart(2, "0")}`;
  startBtn.innerText = "Start";
};

lapBtn.onclick = function(){
    let li = document.createElement("li");
    li.innerText = timer.innerText;
    logs.append(li);
}

resetBtn.onclick = function(){
    logs.innerHTML = ""
}