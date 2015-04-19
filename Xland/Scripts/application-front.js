
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
        
        if (id) {
            $("#preload").show();
            $.getJSON('/project/info2/' + id).complete(function (data) {

                if (data) {
                    
                    //$("#project").html(data.responseText);

                    var rendered = "";
                    var template = $("#template-project").html();
                    //rendered = Mustache.render(template, { name: itemType });

                    rendered = Mustache.render(template, data.responseJSON);

                    $("#projectContainer .container-fluid").html(rendered);

                    $("#preload").hide();

                    $("body").addClass("project-open");
                    $("#projectContainer").addClass("open");

                    App.Plugins.CBPGridGalleryInit();

                    // Facebook
                    FB.XFBML.parse();
                    
                    $(".fb-like").attr("data-href", window.location.href + "/Home/Info2/" + id);
                    $(".twitter-share-button").attr("href", window.location.href + "/Home/Info2/" + id);
                }

                $(".close-project").off("click").on("click", function () {
                    $("#projectContainer").removeClass("open");
                    $("#project").empty();
                    $("body").removeClass("project-open");
                });
            });
        }

        
    },

    // Plugins
    // ----------------------------------
    Plugins: {

        // Initialize Plugins
        // ------------------------------
        Init: function () {
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