const numbersSumElement = document.getElementById('numbersSum');
const numbersListElement = document.getElementById('numbersList');
const counterElement = document.getElementById('counter');
const notSumText = "Not summed";

const clearNumberEndpoint = '/clearNumbers';
const addNumberEndpoint = '/addNumber';
const sumNumberEndpoint = '/sumNumbers';
const getNumbersEndpoint = '/getNumbers';


document.addEventListener('DOMContentLoaded', function () {  
    updateNumbersList();
});

async function clearNumbers() {
    await fetch(clearNumberEndpoint, { method: 'POST' });
    updateNumbersList();
    numbersSumElement.innerHTML = notSumText;
}

async function addNumber() {
    await fetch(addNumberEndpoint, { method: 'POST' });
    updateNumbersList();
}

async function sumNumbers() {
    const response = await fetch(sumNumberEndpoint);
    const data = await response.json();
    if (data.sum === 0) {
        numbersSumElement.innerText = notSumText;
    }
    else {
        numbersSumElement.innerText = `${data.sum}`;
    }
}

async function updateNumbersList() {
    const response = await fetch(getNumbersEndpoint);
    const data = await response.json();
    numbersListElement.innerHTML = '&nbsp;';
    data.numbers.forEach(number => {    
        const listItem = document.createElement('span');
        listItem.textContent = number;
        listItem.style.marginRight = '5px';
        listItem.style.background = 'lightgrey'; 
        listItem.style.padding = '10px'; 
        numbersListElement.appendChild(listItem);
    });
    counterElement.innerHTML = data.numbers.length;
}


