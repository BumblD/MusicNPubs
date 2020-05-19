<template>

<div style="height: 800px; width: 100%">
    <div style="height: 400px overflow: auto;">
    </div>
    <l-map
      :zoom="zoom"
      :center="center"
      :options="mapOptions"
      style="height: 100%"
      @update:center="centerUpdate"
      @update:zoom="zoomUpdate"
    >
    <l-tile-layer
        :url="url"
      />
    <l-marker 
        v-for="marker in markers"
        :key="marker.id"
        :lat-lng="marker.position"
        @click="innerClick(marker.id)"
        
      >
    <l-tooltip :content="marker.tooltip"/>
      </l-marker>
    </l-map>
  </div>

</template>

<script>
import { latLng } from "leaflet";
import { LMap, LTileLayer, LMarker, LPopup, LTooltip } from "vue2-leaflet";
import { Icon }  from 'leaflet'
import 'leaflet/dist/leaflet.css'
import Vue from 'vue'
import router from '../router/index.js'
import axios from 'axios'
// this part resolve an issue where the markers would not appear
delete Icon.Default.prototype._getIconUrl;

Icon.Default.mergeOptions({
    iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
    iconUrl: require('leaflet/dist/images/marker-icon.png'),
    shadowUrl: require('leaflet/dist/images/marker-shadow.png')
});
export default {
  name: "Example",
  components: {
    LMap,
    LTileLayer,
    LMarker,
    LPopup,
    LTooltip
  },
  data() {
    return {
      zoom: 13,
      markers: [
        /*{
          id: 'm1',
          position: { lat: 54.899316, lng: 23.906685 },
          tooltip: 'tooltip for marker1',
        },
        {
          id: 'm2',
          position: { lat: 54.899316, lng: 23.908685 },
          tooltip: 'tooltip for marker2',
        },
        {
          id: 'm3',
          position: { lat: 54.899316, lng: 23.909685 },
          tooltip: 'tooltip for marker3',
        },
        {
          id: 'm4',
          position: { lat: 54.899316, lng: 23.900685 },
          tooltip: 'tooltip for marker4',
        },*/
      ],
      center: latLng(54.899316, 23.904685),
      url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
      withPopup: latLng(54.899316, 23.904685),
      currentZoom: 11.5,
      currentCenter: latLng(54.899316, 23.904685),
      showParagraph: false,
      mapOptions: {
        zoomSnap: 0.5
      }
    };
  },
  async beforeMount () {
    // fetch data from api
    await this.Load();
  },
  methods: {
    Load: async function () {
      var me = this;
        await axios.get('https://localhost:44341/api/Bar/GetAllBars')
      .then(function(response){
          for(var i = 0; i < response.data.length; i++)
          {
            var tempobj = {};
            tempobj.position = {lat: response.data[i].lat, lng: response.data[i].lng};
            tempobj.id = response.data[i].barID;
            tempobj.tooltip = response.data[i].name;
            me.markers.push(tempobj);
            console.log(response.data[i]);
            console.log(tempobj);
          }
      })   
     /* await axios.get('https://localhost:44341/api/bars/getallbars')
      .then(response => (this.markers = response.data))      */
    },
    zoomUpdate(zoom) {
      this.currentZoom = zoom;
    },
    centerUpdate(center) {
      this.currentCenter = center;
    },
    async innerClick(markerid) {
      var me = this;
      var tempobj = {};
      await axios.get('https://localhost:44341/api/Bar/GetBars/'+markerid)
      .then(function(response){
        tempobj = response.data[0];
      })     
      console.log("clicked" + markerid)
      router.push({ name: 'BarDetails', params: { barID: markerid , bar: tempobj} })
    }
  }
};
</script>

<style>

</style>
