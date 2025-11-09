function createTable(n) {
    const table = document.createElement('table');
    const headerRow = document.createElement('tr');
    headerRow.appendChild(document.createElement('th')); // Top-left empty cell

    const randomNumbers = [];
    while (randomNumbers.length < n) {
        const num = Math.floor(Math.random() * 99) + 1;
        if (!randomNumbers.includes(num)) {
            randomNumbers.push(num);
        }
    }

    randomNumbers.forEach(num => {
        const th = document.createElement('th');
        th.textContent = num;
        headerRow.appendChild(th);
    });
    table.appendChild(headerRow);

    randomNumbers.forEach(rowNum => {
        const row = document.createElement('tr');
        const th = document.createElement('th');
        th.textContent = rowNum;
        row.appendChild(th);

        randomNumbers.forEach(colNum => {
            const td = document.createElement('td');
            const product = rowNum * colNum;
            td.textContent = product;
            td.className = (product % 2 === 0) ? 'even' : 'odd';
            td.addEventListener('click', sumOnClick);
            row.appendChild(td);
        });

        table.appendChild(row);
    });

    return table;
}

function sumOnClick() {
    let row = this.parentNode;
    let cells = row.querySelectorAll('td');
    let sum = 0;
    cells.forEach(cell => {
        sum += parseInt(cell.textContent, 10);
        //if (!isNaN(val)) sum += val;
    });
    alert("Sum of the row is: " + sum);
}

function init() {
    let n = parseInt(prompt("Enter the number of rows and columns (5-20):"), 10);
    if (isNaN(n) || n < 5 || n > 20) {
        n = 10; // default value
        const info = document.createElement('p');
        info.textContent = `Invalid input. Defaulting to n=${n}.`;
        document.body.appendChild(info);
    }

    const table = createTable(n);
    document.body.appendChild(table);
}

window.onload = init;