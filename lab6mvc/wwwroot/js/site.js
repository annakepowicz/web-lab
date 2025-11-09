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
            row.appendChild(td);
        });

        table.appendChild(row);
    });

    return table;
}

function initTable() { // Zmieniona nazwa, by odróżnić od nowej 'init'
    let n = parseInt(prompt("Enter the number of rows and columns (5-20):"), 10);
    if (isNaN(n) || n < 5 || n > 20) {
        n = 10; // default value
        const info = document.createElement('p');
        info.textContent = `Invalid input. Defaulting to n=${n}.`;
        document.body.appendChild(info);
    }

    const table = createTable(n);
    document.getElementById("tableContainer").appendChild(table);
}

function initDrawingCanvas(canvas) {
    if (!canvas || !canvas.getContext) return;

    const ctx = canvas.getContext('2d');
    
    const width = canvas.width;
    const height = canvas.height;

    const corners = [
        { x: 0, y: 0 },         // Górny lewy róg
        { x: width, y: 0 },     // Górny prawy róg
        { x: width, y: height },// Dolny prawy róg
        { x: 0, y: height }     // Dolny lewy róg
    ];


    function drawLines(mouseX, mouseY) {
        ctx.clearRect(0, 0, width, height); 

        ctx.strokeStyle = '#851a80ff'; 
        ctx.lineWidth = 2;           

        ctx.beginPath(); 
        
        corners.forEach(corner => {

            ctx.moveTo(corner.x, corner.y); 
            ctx.lineTo(mouseX, mouseY);   
        });

        ctx.stroke();
    }


     // Obsługa ruchu myszy (mousemove)
    function handleMouseMove(event) {
        // getBoundingClientRect() daje nam pozycję kanwy względem okna przeglądarki.
        const rect = canvas.getBoundingClientRect();
        
        // Obliczamy współrzędne myszy wewnątrz płótna:
        const mouseX = event.clientX - rect.left;
        const mouseY = event.clientY - rect.top;

        drawLines(mouseX, mouseY);
    }


    //Obsługa opuszczenia kanwy przez kursor (mouseleave)
    function handleMouseLeave() {
        // Wyczyść całe płótno
        ctx.clearRect(0, 0, width, height);
    }

    // Dodanie słuchaczy zdarzeń
    canvas.addEventListener('mousemove', handleMouseMove);
    canvas.addEventListener('mouseleave', handleMouseLeave);
}


function initAll() {
    // Inicjalizacja tabeli 
    initTable(); 

    // Znalezienie wszystkich elementów <canvas> i inicjalizacja
    const canvases = document.querySelectorAll('.drawingCanvas');
    canvases.forEach(initDrawingCanvas);
}

window.onload = initAll;