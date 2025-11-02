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

// 1. Znalezienie wszystkich elementów <canvas> i inicjalizacja
const canvases = document.querySelectorAll('.drawingCanvas');
canvases.forEach(initDrawingCanvas);