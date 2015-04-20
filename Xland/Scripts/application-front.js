
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

                    window.setTimeout(function () {
                        $("#preload").hide();
                        $("body").addClass("project-open");
                        $("#projectContainer").removeClass("closed").addClass("open");
                        $("div.cs-select").addClass("disabled");
                    }, 800);

                    App.Plugins.CBPGridGalleryInit();

                    // Facebook
                    FB.XFBML.parse();
                    $(".fb-like").attr("data-href", window.location.href + "Project/Info/" + id);

                    // Twitter
                    twttr.widgets.createShareButton(
                      window.location.href + "Project/Info/" + id,
                      document.getElementById('twitter-button'),
                      {
                          count: 'none',
                          text: 'Xland - ' + $(".project-title").text()
                      });

                    // Pinterest
                    window.parsePinBtns(document.getElementById('#pinterest-button'));

                }

                $(".close-project").off("click").on("click", function () {
                    $("#projectContainer").removeClass("open").addClass("closed");
                    $("#project").empty();
                    $("body").removeClass("project-open");
                    $("div.cs-select").removeClass("disabled");
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
                $("#about").addClass("open");
                $("div.cs-select").addClass("disabled");
                return false;
            });
            $("#about .nav-close").on("click", function () {
                $("#about").removeClass("open");
                $("div.cs-select").removeClass("disabled");
            });

            // Show hide close project btn
            $("#projectContainer").on("scroll", function () {
                var offsetY = $("#projectContainer").scrollTop();

                if (offsetY > 40) {
                    $(".close-project-mobile").addClass("active");
                } else {
                    $(".close-project-mobile").removeClass("active");
                }
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