
var App = {

    // Initialize
    // ------------------------------
    Init: function () {
        //this.Global.Init();
        //this.Utils.Init();
        this.Plugins.Init();
    },

    // Utilities
    // ----------------------------------
    Utils: {

        // Initialize Utils
        // ------------------------------
        Init: function () {
        }

    },

    // Plugins
    // ----------------------------------
    Plugins: {

        // Initialize Plugins
        // ------------------------------
        Init: function () {

            $('.datepicker').datepicker();


            $("#geocomplete").geocomplete({
                map: "#mapPreview",
                details: ".geocomplete-inputs",
                detailsAttribute: "data-geo",
                markerOptions: {
                    draggable: true
                }
            });

            
            //Project_LongDescription

            tinymce.init({
                selector: ".tinymce",
                theme: "modern",
                width: 700,
                height: 300,
                plugins: [
                     "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
                     "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
                     "save table contextmenu directionality emoticons template paste textcolor"
                ],
                toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | l      ink image | print preview media fullpage | forecolor backcolor emoticons",
                style_formats: [
                     { title: 'Bold text', inline: 'b' },
                     { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
                     { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
                     { title: 'Example 1', inline: 'span', classes: 'example1' },
                     { title: 'Example 2', inline: 'span', classes: 'example2' },
                     { title: 'Table styles' },
                     { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
                ]
            });

        }

    },

    GeoCoding : 
    {
        Init: function()
        {
            
            $("#find").click(function () {
                $("#geocomplete").trigger("geocode");
            });
            
            $("#geocomplete").bind("geocode:dragged", function (event, latLng) {
                $('input[name="Project.Lat"]').val(latLng.lat());
                $('input[name="Project.Long"]').val(latLng.lng());
                $("#reset").show();
            });


            $("#reset").click(function () {
                $("#geocomplete").geocomplete("resetMarker");
                $("#reset").hide();
                return false;
            });
        }
        
    },

    // Global functions that always run on site
    // ------------------------------
    Global: {

       
    }


};

// Run Application
// ------------------------------

$(document).ready(function () {
    App.Init();

    
});

jQuery(window).on('load', function () {
    $('.gallery-photos').masonry({
        columnWidth: 300,
        itemSelector: '.item',
        "gutter": 10
    });

    $(".btn-delete").on("click", function () {

        var item = this;

        var photoId = $(this).data("id");

        $.post("/PhotoGallery/DeletePhoto/" + photoId,
            function (msg) {

                console.log("msg: " + msg);
                $(".gallery-photos").find("[data-photo-id='" + photoId + "']").remove();
                $(".gallery-photos").masonry('reloadItems');

            }
        );
    });

    $(".main-photo").on("change", function () {
        
        var that = this;
        var id = $(that).parents('.photo').data('photo-id');

        $(".main-photo").not(that).each(function () {
            $(this).attr('checked', false);
        });

        if(id !== "" || id !== 'undefined') {
            $.post("/Photo/SetAsMainPhoto/" + id, function (msg) {
            });
        }
    });

    $('.btn-save-photo').on('click', function () {

        var id = $(this).data('id');
        var title = $(this).parents('.photo').find('input[name="title"]').val();
        var text = $(this).parents('.photo').find('textarea').val();

        if ((id !== "" || id !== 'undefined') && text !== "") {
            //var str = text.substring(1, text.length - 1);
            //var json = JSON.stringify(text);
            
            $.post("/photo/SavePhotoText/" + id + "?title=" + title + "&text=" + text, function () {

            });
        }
    });
});
