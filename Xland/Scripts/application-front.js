
var App = {

    // Initialize
    // ------------------------------
    Init: function (id) {
        this.Global.Init();
        this.Language.Init();
        this.Plugins.Init();
        this.ProjectInfoData(id);
    },

    Language: {

        Init: function () {
            $("#select-language a").on("click", function () {

                var culture = $(this).data('culture');
                //populate hidden fields
                $('input[name="culture"]').val(culture);

                $(this).parents("form").submit(); // post form
            });
        }
        
    },

    ProjectInfoData: function (id) {
        
        $("#projectContainer .close-project").on("click", function () {
            $("#projectContainer").removeClass("open");
        });
        if (id) {
            $("#preload").show();
            $.getJSON('/project/info2/' + id).complete(function (data) {

                if (data) {
        
                    $("#projectContainer").addClass("open");
                    $("#projectContent").html(data.responseText);
                    $("#preload").hide();
                    App.Plugins.CBPGridGalleryInit();

                    $(".fb-like").attr("data-href", "http://localhost:63210/Home/Info/" + id);
                    $(".twitter-share-button").attr("href", "http://localhost:63210/Home/Info/" + id);
                }
            });
        }
    },

    // Plugins
    // ----------------------------------
    Plugins: {

        // Initialize Plugins
        // ------------------------------
        Init: function () {

            /*
            $('#photoTiles').isotope({
                
                // layout mode options
                masonry: {
                    columnWidth: '.grid-sizer'
                },
                itemSelector: '.mini-item'
            });

            $('.isotope').isotope({
                itemSelector: '.item',
                masonry: {
                    columnWidth: 300
                }
            });

            */

            (function () {
                [].slice.call(document.querySelectorAll('select.cs-select')).forEach(function (el) {
                    var options = {
                        onChange : selectChanged
                    }
                    new SelectFx(el, options);
                });
            })();

            
        },

        CBPGridGalleryInit: function () {

            if ($("#grid-gallery").length > 0) {
                new CBPGridGallery(document.getElementById('grid-gallery'));
            }
        }

    },

    // Global functions that always run on site
    // ------------------------------
    Global: {

        Init: function () {

            $(".about").on("click", function () {
                //$("#about").show('blind', null, 400)
                $("#about").addClass("open");
                return false;
            });
            $("#about .fa-close").on("click", function () {
                //$(this).parents("#about").hide('blind', null, 400);
                $("#about").removeClass("open");
            });
            
        }
       
    }


};

// Run Application
// ------------------------------


selectChanged = function (e) {

    if (e == "undefined" || e == "")
        return;

    var category = parseInt(e);

    xland.showCategory(category);
}

$(document).ready(function () {

    var id = $("#pid").val();

    App.Init(id);

});