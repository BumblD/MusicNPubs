<template>
  <div>
    <md-card>
      <div class="md-layout md-gutter">
        <div class="md-layout-item" style="display: flex;align-items: center;">
          <span class="md-title">Mass shooting hits 2020</span>
          <md-button class="md-icon-button md-dense md-accent">
            <md-icon>clear</md-icon>
          </md-button>
        </div>

        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-field>
            <label for="playlists">Playlist</label>
            <md-select v-model="playlists" name="playlists" id="playlists" md-dense>
              <md-option value="empty"></md-option>
              <md-option value="mass">Mass shooting hits 2020</md-option>
              <md-option value="bang">Bang bang ur ded</md-option>
            </md-select>
          </md-field>
        </div>

        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-checkbox v-model="boolean">Leisti lankytojams siūlyti dainas</md-checkbox>
        </div>
      </div>

      <div>
        <md-divider></md-divider>
        <md-list>
          <md-list-item>
            <span class="md-list-item-text">Foster the People - Pumped Up Kicks</span>
            <md-button class="md-icon-button md-dense md-primary">
              <md-icon>arrow_upward</md-icon>
            </md-button>
             <md-button class="md-icon-button md-dense md-accent">
              <md-icon>arrow_downward</md-icon>
            </md-button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <md-button class="md-icon-button md-dense md-accent">
              <md-icon>block</md-icon>
            </md-button>
            <md-button class='md-raised md-primary align-right button-down' v-on:click='Remove'>Šalinti</md-button>
          </md-list-item>

          <md-divider></md-divider>
          <md-list-item>
            <span class="md-list-item-text">Europe - The Final Countdown</span>
            <md-button class="md-icon-button md-dense md-primary">
              <md-icon>arrow_upward</md-icon>
            </md-button>
             <md-button class="md-icon-button md-dense md-accent">
              <md-icon>arrow_downward</md-icon>
            </md-button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <md-button class="md-icon-button md-dense md-accent">
              <md-icon>block</md-icon>
            </md-button>
            <md-button class='md-raised md-primary align-right button-down' v-on:click='Remove'>Šalinti</md-button>
          </md-list-item>

          <md-divider></md-divider>
          <md-list-item>
            <span class="md-list-item-text">Survivor - Eye Of The Tiger</span>
            <md-button class="md-icon-button md-dense md-primary">
              <md-icon>arrow_upward</md-icon>
            </md-button>
             <md-button class="md-icon-button md-dense md-accent">
              <md-icon>arrow_downward</md-icon>
            </md-button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <md-button class="md-icon-button md-dense md-accent">
              <md-icon>block</md-icon>
            </md-button>
            <md-button class='md-raised md-primary align-right button-down' v-on:click='Remove'>Šalinti</md-button>
          </md-list-item>

          <md-divider></md-divider>
          <md-list-item>
            <span class="md-list-item-text">Queen - Don't Stop Me Now</span>
            <md-button class="md-icon-button md-dense md-primary">
              <md-icon>arrow_upward</md-icon>
            </md-button>
             <md-button class="md-icon-button md-dense md-accent">
              <md-icon>arrow_downward</md-icon>
            </md-button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <md-button class="md-icon-button md-dense md-accent">
              <md-icon>block</md-icon>
            </md-button>
            <md-button class='md-raised md-primary align-right button-down' v-on:click='Remove'>Šalinti</md-button>
          </md-list-item>
          <md-divider></md-divider>

          <md-list-item>
            <span class="md-list-item-text">Bee Gees - Stayin' Alive</span>
            <md-button class="md-icon-button md-dense md-primary">
              <md-icon>arrow_upward</md-icon>
            </md-button>
             <md-button class="md-icon-button md-dense md-accent">
              <md-icon>arrow_downward</md-icon>
            </md-button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <md-button class="md-icon-button md-dense md-accent">
              <md-icon>block</md-icon>
            </md-button>
            <md-button class='md-raised md-primary align-right button-down' v-on:click='Remove'>Šalinti</md-button>
          </md-list-item>
          <md-divider></md-divider>
          <md-list-item>
            <md-button class="md-fab md-mini md-primary center-horizontal">
              <md-icon>add</md-icon>
            </md-button>
          </md-list-item>
          <md-divider></md-divider>
        </md-list>
      </div>

      <div class="md-layout-item md-layout md-gutter">
        <div class="md-layout-item">
          <md-button class="md-fab md-primary">
            <md-icon>skip_previous</md-icon>
          </md-button>
          <md-button class="md-fab md-primary">
            <md-icon>play_arrow</md-icon>
          </md-button>
          <md-button class="md-fab md-primary">
            <md-icon>skip_next</md-icon>
          </md-button>
        </div>
        <div class="md-layout-item">
          <md-button class="md-fab md-primary">
            <md-icon>shuffle</md-icon>
          </md-button>
        </div>
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
    /* OpenMap() { //Might make map to center on this bar if im feeling good
      router.push({ name: 'Map', params: {} })
    },
    OpenReviews(){
      router.push({ name: 'Reviews', params: { barID : this.barID} })
    } */
  }
}
</script>
<style scoped>
.md-card {
  width: 60%;
  display: inline-block;
  vertical-align: top;
  padding: 2%;
}

.center-horizontal {
  display: block;
  margin-left: auto;
  margin-right: auto;
}

.button-down {
  padding-top: 0.1%;
}

.center-vertical {
  display: flex !important;
  justify-content: center !important;
  align-items: center !important;
}
</style>
