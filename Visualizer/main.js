var cells;
var player;

function setup(){
  createCanvas(640,480);
  cells = [];
  player = undefined;
  refreshData();
  setInterval(refreshData, 1000);
}

function draw(){
  background(51);
  noFill();
  stroke(255);
  for(var i = 0; i < cells.length; i++){
    var cell = cells[i];

    rect(cell.x,cell.y,40,40);
  }

  if(player){
    fill(0,255,0);
    stroke(0);
    ellipse(player.x,player.y,20,20);
  }
}

function refreshData(){
  loadJSON('./data.json', function(data){
    if(data.cells) cells = data.cells;
    if(data.player) player = data.player;
  });
}
