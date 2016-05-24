/**
 * Created by alice on 10/05/2016.
 */

    function allowDrop(ev) {
        ev.preventDefault();
    }

    function drag(ev) {
        ev.dataTransfer.setData("text", ev.target.id);

        var data = ev.target;

        if (data.classList.contains("arrangedTiles")) {
            $('.arrangedTiles').addClass('hvr-wobble-horizontal');
        }    }
        
           //var img=document.getElementById(ev.target.id).cloneNode(true);
           // //img.style.transform = "scale(0.5)";
           // $(img).attr('class','halfSize');
           // ev.dataTransfer.setDragImage(img,0,0);
        }

    //function drop(ev) {
    //    ev.preventDefault();
    //                $('img').removeClass('hvr-wobble-horizontal');


    //    var data = ev.dataTransfer.getData("text");
    //    var parent=findAncestor(ev.target,"productGridItem");
    //    if (parent == null)
    //    {
    //        ev.target.appendChild(document.getElementById(data));
    //        document.getElementById(data).classList.add('hvr-pulse');
    //    } else {
    //        //swaps the elements
    //        var replaced = parent.innerHTML;
    //        $('#imageContainter').append(replaced);
    //        //TODO: empty parent and add the new tile
    //        parent.innerHTML="";
    //        parent.appendChild(document.getElementById(data));
    //    }
    //      if ($('#selectedTileStyle').is(':empty')){
    //        var placeholder = $('<img class="previewImage" src="http://placehold.it/299x299"/>');
    //        $('#selectedTileStyle').append(placeholder);
    //    }
//}
    function drop(ev) {
        //TODO:scale down every pic
        //TODO:duplicate all images
        ev.preventDefault();

                    $('img').removeClass('hvr-wobble-horizontal');


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
        if (document.getElementById(data).classList.contains('arrangedTiles')){
            document.getElementById(data).classList.remove('arrangedTiles');
        }
          if ($('#selectedTileStyle').is(':empty')){
            var placeholder = $('<img class="previewImage" src="http://placehold.it/299x299"/>');
            $('#selectedTileStyle').append(placeholder);
        }
        setTimeout(function(){
            $('.hoverZoomLink').removeClass('hvr-pulse');
        }, 1500);
    }
//find the ancestor of the image
function findAncestor (el, cls) {
    while ((el = el.parentElement) && !el.classList.contains(cls));
    return el;
};