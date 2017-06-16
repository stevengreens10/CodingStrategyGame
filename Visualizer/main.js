var cells;
var playerData;
var turn = 0;
var scl = 10;
var lastTurnX = 0;
var lastTurnY = 0;
var len0= 0;
var len1=0;
var FPS = 3;
var stopped = false;

function setup(){
  cells = [];
  playerData = undefined;
  refreshData();
  createCanvas(scl*len0+1,scl*len1+1);
  createP("");
  var restart = createButton("Restart");
  var rewind = createButton("<<");
  var slow = createButton("-Speed");
  var stopButton = createButton("Stop");
  var fast = createButton("+Speed");
  var fastForward = createButton(">>")


  restart.mousePressed(function(){
	  stopped = false;
	  turn = 0;
    drawMaze();
  });

  rewind.mousePressed(function(){
    turn-=25;
    if(turn < 0) turn = 0;
  });

  slow.mousePressed(function(){
    FPS /= 2;
	if (FPS < 1) FPS = 1;
  });

  fast.mousePressed(function(){
	  if (stopped)
	  {
		  FPS = 3;
		  stopped = false;
	  }
	  else
	  {
		  FPS *= 2;
	  }
  });

  fastForward.mousePressed(function(){
    turn+=25;
  });

  stopButton.mousePressed(function(){
	  stopped = true;
  });


  drawMaze();


}

function drawMaze(){
  background(0);
  noFill();
  stroke(255);
  for(var i = 0; i < cells.length; i++){
    var cell = cells[i];
    var x = cell.x*scl;
    var y = cell.y*scl;
    //Top left to top right
    if(cell.Walls[0]) line(x,y,x+scl,y);
    //Top right to bottom right
    if(cell.Walls[1]) line(x+scl,y,x+scl,y+scl);
    //Bottom right to bottom left
    if(cell.Walls[2]) line(x+scl,y+scl,x,y+scl);
    //Bottom left to top left
    if(cell.Walls[3]) line(x,y+scl,x,y);
  }
}

function draw(){


  frameRate(FPS);

  if (!stopped) {
  //if(turn >= 1000) turn = 0;
  var player = playerData[turn].Player;
  var x = player.currentCell.x;
  var y = player.currentCell.y;
  console.log("Turn #", playerData[turn].turn, "(",x, ",",y,")");

  fill(0,0,0);
  stroke(0);
  ellipse(lastTurnX * scl + scl/2, lastTurnY*scl + scl/2, scl/1.5, scl/1.5);
  lastTurnX = player.currentCell.x;
  lastTurnY = player.currentCell.y;

  fill(0,255,0);
  ellipse(x * scl + scl/2, y * scl + scl/2, scl/1.5, scl/1.5);


  turn++;
  }


}

function refreshData(){
	var data = JSON.parse(document.getElementById("maze").innerText);
    for(var i = 0; i < data.Cells.length; i++){
      for(var j = 0; j < data.Cells[i].length; j++){
        cells.push(data.Cells[i][j]);
      }
    }
	len0 = data.Cells.length;
	len1=data.Cells[1].length;


    data = JSON.parse(document.getElementById("player").innerText);
    if(data) playerData = data;
}
