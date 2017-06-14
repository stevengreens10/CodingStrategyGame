var cells = [];
var player = undefined;

function setup(){
  createCanvas(640,480);
  refreshData();
  setInterval(refreshData, 1000);
}

function draw(){
  background(51);

  for(var i = 0; i < cells.length; i++){
    var cell = cells[i];
  }
}

function refreshData(){
  var data = loadJSON('./data.json');
  cells = data.cells;
  player = data.player;

}
