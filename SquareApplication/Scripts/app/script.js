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
    tile = selected.id;
    //selected.classList.add('doubleSize');
    document.getElementById('selectedTileStyle').innerHTML = '';
    $('#selectedTileStyle').append(selected);
    selected.classList.add('previewImage');
    selected.classList.remove('arrangedTiles');

    index++;
}


/**
 * Created by Jakob on 20/05/2016.
 */
$(function () {
    $('#modalCancelBtn').click(function () {
        $('#modal-container').modal('hide');
    });
});

(function () {
    "use strict";

    function modal() {

        var cover = null,
            modalOpenButtons = null,
            modalCloseButtons = null,
            openModalWindow = null;

        var init = function () {
            cover = document.querySelector(".cover"),
            modalOpenButtons = document.querySelectorAll(".modal-btn-open"),
            modalCloseButtons = document.querySelectorAll(".modal-btn-close");

            for (var i = 0; i < modalCloseButtons.length; i++) {
                var modalCloseButton = modalCloseButtons[i];
                modalCloseButton.addEventListener("click", closeModal);
            }

            var max = modalOpenButtons.length;
            for (var i = 0; i < max; i++) {
                var modalOpenButton = modalOpenButtons[i];
                modalOpenButton.addEventListener("click", openModal);
            }

            // cover.onclick = closeModal;

        }

        var openModal = function (e) {
            e.preventDefault();
            var modalButton = e.target;
            var modalTarget = modalButton.getAttribute("href");
            openModalWindow = document.querySelector(modalTarget);
            cover.classList.add("open");
            openModalWindow.classList.add("show");
        }


        var closeModal = function () {
            openModalWindow.classList.remove("show");
            cover.classList.remove("open");
        }

        return {
            init: init
        }
    }

    var modal = modal();
    modal.init();

    $(function () {
        // Initialize numeric spinner input boxes
        //$(".numeric-spinner").spinedit();

        // Initalize modal dialog
        // attach modal-container bootstrap attributes to links with .modal-link class.
        // when a link is clicked with these attributes, bootstrap will display the href content in a modal dialog.
        $('body').on('click', '.modal-link', function (e) {
            e.preventDefault();
            $(this).attr('data-target', '#modal-container');
            $(this).attr('data-toggle', 'modal');
        });

        // Attach listener to .modal-close-btn's so that when the button is pressed the modal dialog disappears
        $('body').on('click', '.modal-close-btn', function () {
            $('#modal-container').modal('hide');
        });

        //clear modal cache, so that new content can be loaded
        $('#modal-container').on('hidden.bs.modal', function () {
            $(this).removeData('bs.modal');
        });

        $('#CancelModal').on('click', function () {
            return false;
        });
    });

})();