/**
 * Created by alice on 10/05/2016.
 */

    function allowDrop(ev) {
        ev.preventDefault();
    }

    function drag(ev) {
        ev.dataTransfer.setData("text", ev.target.id);


           //var img=document.getElementById(ev.target.id).cloneNode(true);
           // //img.style.transform = "scale(0.5)";
           // $(img).attr('class','halfSize');
           // ev.dataTransfer.setDragImage(img,0,0);
        }

    function drop(ev) {
              ev.preventDefault();
        var data = ev.dataTransfer.getData("text");
        var parent=findAncestor(ev.target,"productGridItem");
        if (parent == null)
        {
            ev.target.appendChild(document.getElementById(data));
            document.getElementById(data).classList.add('hvr-pulse');
        } else {
            //swaps the elements
            var replaced = parent.innerHTML;
            $('#imageContainter').append(replaced);
            //TODO: empty parent and add the new tile
            parent.innerHTML="";
            parent.appendChild(document.getElementById(data));
        }
          if ($('#selectedTileStyle').is(':empty')){
            var placeholder = $('<img class="previewImage" src="http://placehold.it/299x299"/>');
            $('#selectedTileStyle').append(placeholder);
        }
    }
//find the ancestor of the image
function findAncestor (el, cls) {
    while ((el = el.parentElement) && !el.classList.contains(cls));
    return el;
};