/**
 * Created by alice on 11/05/2016.
 */
var angle=90;
var tile=-1;
var index=0;
function rotate(){
    var img= document.getElementById(tile);

    //going with inline styling so it could be applied multiple times to the same element
    img.style.transform = 'rotate('+angle +'deg)';
    img.style.transition = '1s';
    angle+=90;
}
function invert() {
    var img= document.getElementById(tile);
    img.classList.toggle('invertColors');
}


function removeGrid(){
    $('.productGridItem').toggleClass('noBorder');
}
//orange overlay needs to be removed at some point
function selectTile(image){
    //two elements on the page with same id
    //TODO:add image overlay to show which one is currently selected
    //image.setAttribute('class','orangeOverlay');
    var selected=image.cloneNode(true);
    selected.setAttribute('id','copy'+index);
    //created a copy which can serve as id
    //new problem -  it triggers the same events even after in grid
    tile=selected.id;
    selected.classList.add('doubleSize');
    document.getElementById('selectedTileStyle').innerHTML='';
    $('#selectedTileStyle').append(selected);
    index++;
}
