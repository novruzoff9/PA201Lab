const submitBtn = document.getElementById("submit");
const questions = document.getElementsByClassName("question");
const totalPoint = document.getElementById("TotalPoint");
const timer = document.getElementById("timer");

let minute = 0;
let second = 2;
let millisecond = 10;

let timerInterval = setInterval(() => {
  millisecond--;
  if (minute == 0 && second == 0 && millisecond == 0) {
    submitBtn.disabled = true;
    clearInterval(timerInterval);
  }
  if (millisecond == -1) {
    second--;
    millisecond = 10;
  }
  if (second == -1) {
    minute--;
    second = 59;
  }
  timer.innerText = `${minute.toString().padStart(2, "0")}:${second
    .toString()
    .padStart(2, "0")}:${millisecond.toString().padStart(2, "0")}`;
}, 100);

submitBtn.onclick = function () {
  let trueanswers = 0;
  //let selectedInputs = document.querySelectorAll("input[type='radio']:checked");
  for (let index = 0; index < questions.length; index++) {
    let right = questions[index].getAttribute("data-right");
    let checkedVariant = questions[index].querySelector(
      "input[type='radio']:checked"
    );
    if (right == checkedVariant.value) {
      trueanswers++;
    }
  }

  totalPoint.innerText = `Duzgun cavablarin sayi : ${trueanswers}`;
};
