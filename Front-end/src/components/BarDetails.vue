<template>
  <div id="app">
    <md-card>
        <div>
            <label>
            Baro Pavadinimas 
            <br>
            {{barInfo.name}}
            </label>
        </div>      
        <md-divider class="md-inset"></md-divider>
        <div>
            Baro Aprašymas
            <br>
            {{barInfo.description}}
        </div>
        <br>
        <div>
            <md-button
            class="md-raised md-primary align-left center-vertical"
            v-on:click="OpenMap"
            >Rodyti Žemėlapyje</md-button> 
        </div>
        <div>
            <md-button
            class="md-raised md-primary align-left center-vertical"
            v-on:click="OpenPlaylist"
            >Rodyti baro grojaraštį</md-button> 
        </div>
        <div>
            <md-button
            class="md-raised md-primary align-left center-vertical"
            v-on:click="OpenReviews"
            >Peržiūrėti atsiliepimus</md-button> 
        </div>
    </md-card>
  </div>
</template>

<script>
import Vue from 'vue'
import VueMaterial from 'vue-material'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
import router from '../router/index.js'
import axios from 'axios'
Vue.use(VueMaterial)
export default {
  name: 'App',  
  data: function () {
    return {
      barID: this.$route.params.barID,
      barInfo: this.$route.params.bar
    }
  },
  methods: {
    OpenMap() { //Might make map to center on this bar if im feeling good
      router.push({ name: 'Map', params: {} })
    },
    OpenReviews(){
      router.push({ name: 'Reviews', params: { barID : this.barID} })
    }
  }
}
</script>
<style scoped>
.md-card {
  width: 30%;
  display: inline-block;
  vertical-align: top;
  padding: 2%;
}
</style>