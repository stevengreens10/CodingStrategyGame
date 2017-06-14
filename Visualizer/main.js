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
  for(var i = 0; i < cells.length; i++){
    var cell = cells[i];

    //DISPLAY CELLS

  }

  //DISPLAY PLAYER
}

function refreshData(){
  loadJSON('./data.json', function(data){
    if(data.cells) cells = data.cells;
    if(data.player) player = data.player;
  });
}
