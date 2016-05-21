/**
 * Created by alice on 10/05/2016.
 */
(function (){

       for (var i = 1; i <26; i++) {
           var cell = $('<div class="productGridItem" ondrop="drop(event)" ondragover="allowDrop(event)">');
           var id = "cell" + i;
           cell.attr('id', id);
           $('#droppableGrid').append(cell);
       }

    for (var i=1;i<26; i++){
        var img= $('<img class="arrangedTiles" ondragstart="drag(event)" draggable="true" onclick="selectTile(this)">');
        var name= i+".jpg";
        var path="/Assets/images/"+ name;
        img.attr('src', path);
        img.attr('id',name);
        $('#imageContainter').append(img);
    }

    }());
