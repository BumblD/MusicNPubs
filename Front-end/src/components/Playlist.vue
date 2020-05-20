<template>
  <div>
    <md-card>
      <div class="md-layout md-gutter">
        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-field md-clearable>
            <label>Pavadinimas</label>
            <md-input v-model="playlist.name"></md-input>
          </md-field>
        </div>

        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-field>
            <label for="playlists">Grojaraščių sąrašas</label>
            <md-select v-model="playlist" name="playlist" id="playlist" md-dense>
              <div v-for="pl in playlists" v-bind:key="pl.id">
                <md-option :value="pl.id">{{pl.name}}</md-option>
              </div>
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
          <div v-for="s in songs" v-bind:key="s.id">
          <md-list-item>
              <span class="md-list-item-text">{{s.name}}</span>
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
          </div>

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
      barId: 1,
      playlistName: 'European hits',
      playlists: [ { id: 1, name: 'bang bang' }, { id: 2, name: 'ding ding' }, { id: 0, name: '' } ],
      songs: [ { id: 1, name: 'Queen - Don\'t Stop Me Now' }, { id: 2, name: 'Survivor - Eye Of The Tiger' } ],
      playlist: { id: 0, name: '' }
    }
  },
  methods: {
    LoadPlaylists: async function () {
      await axios.get('https://localhost:44341/api/playlist/getbarplaylists/' + this.barId)
        .then(response => (this.playlists = response.data))
    },
    LoadPlaylistSongs: async function () {
      await axios.get('https://localhost:44341/api/playlist/getplaylistsongs/' + this.playlist.id)
        .then(response => (this.songs = response.data))
    }
  },
  async beforeMount () {
    // fetch data from api
    await this.LoadPlaylists()
  },
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
