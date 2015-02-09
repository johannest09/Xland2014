
var App = {

    // Initialize
    // ------------------------------
    Init: function () {
        this.Global.Init();
        this.Language.Init();
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
    console.log("test on change: " + e);

    if (e == "undefined" || e == "")
        return;

    var category = parseInt(e);

    xland.showCategory(category);
}

$(document).ready(function () {

    App.Init();

});