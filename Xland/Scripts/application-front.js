
var App = {

    // Initialize
    // ------------------------------
    Init: function (id) {
        this.Global.Init();
        this.Language.Init();
        this.Plugins.Init();
        this.ProjectInfoData(id);
    },

    CurrentProject: {},

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

        function cutText(text) {
            if (text.length > 200) {
                return text.substring(0, 200) + "...";
            } else {
                return text;
            }
                
        }

        function stripHtml(html) {
            return html.replace(/<(?:.|\n)*?>/gm, '');
        }
        
        if (id) {
            $("#preload").show();
            $.getJSON('/project/info2/' + id).complete(function (data) {

                if (data) {
                    
                    var rendered = "";
                    var template = $("#template-project").html();

                    rendered = Mustache.render(template, data.responseJSON);

                    $("#projectContainer .container-fluid").html(rendered);

                    imagesLoaded($("#grid-gallery"), function () {

                        App.Plugins.CBPGridGalleryInit();

                        $("#preload").hide();
                        $("body").addClass("project-open");
                        $("#projectContainer").removeClass("closed").addClass("open");
                        $("div.cs-select").addClass("disabled");
                    });
                    
                    // Facebook
                    
                    var project = data.responseJSON;
                    var mainPhoto = project.Photos[0].Photo.Path; // If no main photo, take the first

                    $(project.Photos).each(function (i, item) {
                        $(item).each(function (iphoto, photo) {
                            if (photo.Photo.IsMainPhoto) {
                                mainPhoto = photo.Photo.Path;
                            }
                        });
                    });

                    var firstPart = mainPhoto.substring(0, mainPhoto.lastIndexOf("/") + 1);
                    var lastPart = mainPhoto.substring(mainPhoto.lastIndexOf("/") + 1, mainPhoto.length);

                    var URIEncodedPhotoPath = firstPart + encodeURIComponent(lastPart);

                    App.CurrentProject = {
                        ID: id,
                        Title: project.Project.Title,
                        Description: project.Project.Description,
                        Photo: URIEncodedPhotoPath
                    }

                    FB.init({
                        appId: '623955561080687',
                        xfbml: true,
                        version: 'v2.4'
                    });
                    
                    $("#fb-share").off("click").on("click", function () {
                        if (App.CurrentProject.Photo) {
                            FB.ui(
                                {
                                    method: "feed",
                                    link: window.location.href + 'Project/Info/' + id,
                                    caption: App.CurrentProject.Title,
                                    description: cutText(stripHtml(App.CurrentProject.Description)),
                                    picture: App.CurrentProject.Photo
                                },
                                function (response) { }
                            );
                        } else {
                            FB.ui(
                                {
                                    method: "feed",
                                    link: window.location.href + 'Project/Info/' + id,
                                    caption: title,
                                    description: cutText(stripHtml(App.CurrentProject.Description)),
                                },
                                function (response) { }
                            );
                        }
                    });

                    FB.XFBML.parse();

                    // Twitter
                    twttr.widgets.createShareButton(
                      window.location.href + "Project/Info/" + id,
                      document.getElementById('twitter-button'),
                      {
                          count: 'none',
                          text: 'Xland - ' + $(".project-title").text()
                      });

                    // Pinterest
                    //window.parsePinBtns(document.getElementById('#pinterest-button'));
                }

                $(".close-project").off("click").on("click", function () {
                    $("#projectContainer").removeClass("open").addClass("closed");
                    $("#projectContainer .container-fluid").empty();
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

    xland.showCategory(parseInt(e));
}

$(document).ready(function () {

    var id = $("#pid").val();
    App.Init(id);

});