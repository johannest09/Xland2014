﻿
/**
 * xland application
 * 
 * @author Jóhannes Freyr Þorleifsson
 * @copyright (c) 2014
 */
// 64.138621,-21.894722
var defaultZoom = 11;
var defaultLatlng = new google.maps.LatLng(64.152123, -21.816328);
var infowindow;
var markerList = {};
var mapStyle = "/mapstyle.js";

var map;
var overlay;
MyOverlay.prototype = new google.maps.OverlayView();
MyOverlay.prototype.onAdd = function () { }
MyOverlay.prototype.onRemove = function () { }
MyOverlay.prototype.draw = function () { }
function MyOverlay(map) { this.setMap(map); }

var xland = {

    loadMap: function () {
        var MY_MAPTYPE_ID = 'reykjavikMap';

        // Map styling
        $.getJSON("/Scripts/map/mapstyle.js", function (data) {
            var styledMapOptions = { name: 'X-land' };
            var xlandMapType = new google.maps.StyledMapType(data, styledMapOptions);
            map.mapTypes.set(MY_MAPTYPE_ID, xlandMapType);
        });

        // Map config
        var myOptions = {
            center: defaultLatlng,
            zoom: defaultZoom,
            panControl: false,
            zoomControl: true,
            streetViewControl: false,
            mapTypeControl: true,
            mapTypeControlOptions: {
                mapTypeIds: [google.maps.MapTypeId.ROADMAP, MY_MAPTYPE_ID],
                style: google.maps.MapTypeControlStyle.DROPDOWN_MENU
            },
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.SMALL,
                position: google.maps.ControlPosition.LEFT_BOTTOM
            },
            mapTypeId: MY_MAPTYPE_ID
        };

        map = new google.maps.Map(document.getElementById("map-canvas"), myOptions);
        overlay = new MyOverlay(map);
        // create new info window for marker detail pop-up
        infowindow = new google.maps.InfoWindow({ disableAutoPan: true });
        return map;
    },

    //Todo: add a category as a parameter
    loadMarkers: function () {
        var MapContainer = this;
        $.getJSON('/home/GetMarkers', function (data) {
            if (data != null) {
                $.each(data, function (i, item) {
                    MapContainer.loadMarker(item);
                });
            }
        });
    },

    loadMarker: function (markerData) {

        var markerColor;

        // Almenningsrými, Saga, Samkeppnir, Skipulag

        if (markerData.ProjectType === 0) {
            markerColor = "../../../Content/Template/icons/marker_orange.png";
        }
        else if (markerData.ProjectType === 1) {
            markerColor = "../../../Content/Template/icons/marker_green.png";
        }
        else if (markerData.ProjectType === 2) {
            markerColor = "../../../Content/Template/icons/marker_purple.png";
        }
        else if (markerData.ProjectType === 3) {
            markerColor = "../../../Content/Template/icons/marker_yellow.png";
        }
        else {
            markerColor = "../../../Content/Template/icons/marker_green.png";
        }

        var MapContainer = this;
        var Latlng = new google.maps.LatLng(markerData.Lat, markerData.Long);

        // create new marker
        var marker = new google.maps.Marker({
            id: markerData["ID"],
            map: map,
            title: markerData["Title"],
            category: markerData["ProjectType"],
            position: Latlng,
            animation: google.maps.Animation.DROP,
            icon: markerColor
        });

        // add marker to list used later to get content and additional marker information
        markerList[marker.id] = marker;

        // add event listener when marker is clicked
        // currently the marker data contain a dataurl field this can of course be done different
        google.maps.event.addListener(marker, 'click', function () {

            var newCenter = overlay.getProjection().fromLatLngToContainerPixel(marker.position);
            newCenter.y -= 100;
            newCenter = overlay.getProjection().fromContainerPixelToLatLng(newCenter);
            MapContainer.showInfoWindow(marker.id);
            map.panTo(newCenter);
        });

        // add event when marker window is closed to reset map location
        google.maps.event.addListener(infowindow, 'closeclick', function () {
            //map.setCenter(defaultLatlng);
            //map.setZoom(defualtZoom);
        });

    },

    //TODO: Remove this.. to expensive and we can have all the data in the first ajax call!
    showInfoWindow: function (markerId) {
        var marker = markerList[markerId];

        $.getJSON('/home/GetMarkerInfo/' + markerId, function (data) {

            var photo = '';
            if (data['MainPhoto'] !== undefined) {
                photo = '<img src="' + data['MainPhoto'].substring(1) + '?w=150&h=150&mode=carve" class="marker-image" />';
            }
            
            var html = '<div class="infowindow"><h2>' + data['Title'] + '</h2>' + photo + '<a href="/Project/Info/' + markerId + '"' + ' title="Nánar">Nánar</a></div>';

            infowindow.setContent(html);
            infowindow.open(map, marker);
        });

    },

    showCategory: function (category) {
        for (id in markerList) {
            if (category == 4 || markerList[id].category == parseInt(category)) {
                markerList[id].setMap(map);
            }
            else {
                markerList[id].setMap(null);
            }
        }
    }

};

$(document).ready(function () {

    google.maps.event.addListenerOnce(xland.loadMap(), 'idle', function () {
        xland.loadMarkers();
    });

    google.maps.event.addListener(infowindow, 'domready', function () {

        $(".infowindow").find("a").bind("click", function () {
            console.log("open project!");
        });
    });
});