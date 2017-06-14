var cells;
var player;

function setup(){
  createCanvas(501,501);
  cells = [];
  player = undefined;
  refreshData();
  noLoop();
  //setInterval(refreshData, 5000);
}

function draw(){
  background(51);
  noFill();
  stroke(255);
  for(var i = 0; i < cells.length; i++){
    var cell = cells[i];
    var scl = 5;
    var x = cell.x*scl;
    var y = cell.y*scl;
    //Top left to top right
    if(cell.Walls[0] === false) line(x,y,x+scl,y);

    //Top right to bottom right
    if(cell.Walls[1] === false) line(x+scl,y,x+scl,y+scl);
    //Bottom right to bottom left
    if(cell.Walls[2] === false) line(x+scl,y+scl,x,y+scl);
    //Bottom left to top left
    if(cell.Walls[3] === false) line(x,y+scl,x,y);
  }


}

function refreshData(){
	var data = JSON.parse(document.getElementById("maze").innerText);
    for(var i = 0; i < data.Cells.length; i++){
      for(var j = 0; j < data.Cells[i].length; j++){
        cells.push(data.Cells[i][j]);
      }
    }
    redraw();
  

  /*loadJSON('./playerdata.json', function(data){
    if(data[0].Player) player = data[0].Player;
  });*/
}
