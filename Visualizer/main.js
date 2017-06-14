var cells;
var playerData;
var turn = 0;
var scl = 10;

function setup(){
  createCanvas(scl*100+1,scl*100+1);
  cells = [];
  playerData = undefined;
  refreshData();
  background(51);
  noFill();
  stroke(255);
  for(var i = 0; i < cells.length; i++){
    var cell = cells[i];
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

  frameRate(5);
  //setInterval(refreshData, 5000);
}

function draw(){
//  background(51);


  if(turn >= 1000) turn = 0;
  var player = playerData[turn].Player;
  var x = player.currentCell.x;
  var y = player.currentCell.y;
  console.log("Turn #", playerData[turn].turn, "(",x, ",",y,")");
  fill(0,255,0);
  stroke(0);
  ellipse(x * scl + scl/2, y * scl + scl/2, scl, scl);
  turn++;



}

function refreshData(){
	var data = JSON.parse(document.getElementById("maze").innerText);
    for(var i = 0; i < data.Cells.length; i++){
      for(var j = 0; j < data.Cells[i].length; j++){
        cells.push(data.Cells[i][j]);
      }
    }


    data = JSON.parse(document.getElementById("player").innerText);
    if(data) playerData = data;
}
