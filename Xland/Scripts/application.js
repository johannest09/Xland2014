
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

            /*$('.geocomplete input').geocomplete({
                map: "mapPreview"
            });*/

            $("#geocomplete").geocomplete({
                map: "#mapPreview",
                details: ".geocomplete-inputs",
                detailsAttribute: "data-geo",
                markerOptions: {
                    draggable: true
                }
            });
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

            //Project_LongDescription

            tinymce.init({
                selector: "textarea",
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
                     {title: 'Bold text', inline: 'b'},
                     {title: 'Red text', inline: 'span', styles: {color: '#ff0000'}},
                     {title: 'Red header', block: 'h1', styles: {color: '#ff0000'}},
                     {title: 'Example 1', inline: 'span', classes: 'example1'},
                     {title: 'Example 2', inline: 'span', classes: 'example2'},
                     {title: 'Table styles'},
                     {title: 'Table row 1', selector: 'tr', classes: 'tablerow1'}
                ]
            });

           

        }

    },

    // Global functions that always run on site
    // ------------------------------
    Global: {

        // Initialize Global
        // ------------------------------
        Init: function () {

            $('.delete-gallery').on('click', function () {
                var id = $(this).data('id');
                if (id) {
                    var tr = $(this).parent().parent();
                    $.post("/ImageGallery/Delete/" + id, function (data) {
                        $(tr).remove();
                    });
                }
            });
        }
    }

  
};

// Run Application
// ------------------------------

$(document).ready(function () {
    App.Init();
});