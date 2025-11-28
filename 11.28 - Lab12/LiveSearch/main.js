const searchInput = document.getElementById("search");
const ul = document.getElementById("fruits");
// let lis = document.querySelectorAll("#fruits > li");
// let furits = [];
// lis.forEach((li) => {
//   furits.push(li.innerText);
// });

let titles = [];

loadData();

async function loadData() {
  let response = await axios.get("https://jsonplaceholder.typicode.com/posts");
  let data = response.data;
  data.forEach((blog) => {
    let liElement = document.createElement("li");
    liElement.innerText = blog.title;
    titles.push(blog.title);
    ul.append(liElement);
  });

  console.log();
}
searchInput.onkeyup = function () {
  let value = searchInput.value;

  ul.innerHTML = "";
  let containesElements = [];
  titles.forEach((x) => {
    if (x.includes(value)) {
      containesElements.push(x);
    }

  });

  containesElements.forEach((x) => {
    let liElement = document.createElement("li");

    let startIndex = x.indexOf(value);
    let startText = x.substring(0, startIndex);
    let endText = x.substring(startIndex + value.length, x.length);
    let findedText = `${startText}<span style="color:red">${value}</span>${endText}`;
    liElement.innerHTML = findedText;
    ul.append(liElement);
  });
};
