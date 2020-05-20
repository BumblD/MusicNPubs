<template>
  <div>
    <md-card>
      <div class="md-layout md-gutter">
        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-field md-clearable>
            <label>Pavadinimas</label>
            <md-input v-model="selected.name"></md-input>
          </md-field>
        </div>

        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-field>
            <label for="playlists">Grojaraščių sąrašas</label>
            <md-select v-model="selected" md-dense>
                <md-option v-for="pl in playlists" :value="pl" :key="pl.id" > {{ pl.name }} </md-option>
            </md-select>
          </md-field>
        </div>

        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-checkbox v-model="LetChoose">Leisti lankytojams siūlyti dainas</md-checkbox>
        </div>
      </div>

      <div>
        <md-divider></md-divider>
        <md-list>
          <div v-for="s in songs" v-bind:key="s.id">
          <md-list-item>
              <span class="md-list-item-text">{{s.name}}</span>
              <md-button v-on:click="ArrowUp(s.id)" class="md-icon-button md-dense md-primary">
                <md-icon >arrow_upward</md-icon>
              </md-button>
              <md-button v-on:click="ArrowDown(s.id)" class="md-icon-button md-dense md-accent">
                <md-icon >arrow_downward</md-icon>
              </md-button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <md-button v-on:click="Blocked(s.id)" class="md-icon-button md-dense md-accent">
                <md-icon >block</md-icon>
              </md-button>
              <md-button class='md-raised md-primary align-right button-down' v-on:click='Remove'>Šalinti</md-button>
            </md-list-item>
            <md-divider></md-divider>
          </div>
          <md-list-item>
            <md-button v-on:click="CreateNew" class="md-fab md-mini md-primary center-horizontal">
              <md-icon >add</md-icon>
            </md-button>
          </md-list-item>
          <md-divider></md-divider>
        </md-list>
      </div>

      <div class="md-layout-item md-layout md-gutter">
        <div class="md-layout-item">
          <md-button v-on:click="SkipPrevious" class="md-fab md-primary">
            <md-icon >skip_previous</md-icon>
          </md-button>
          <md-button v-on:click="Play" class="md-fab md-primary">
            <md-icon v-show="PlayDisplay">play_arrow</md-icon>
            <md-icon v-show="!PlayDisplay">stop</md-icon>
          </md-button>
          <md-button v-on:click="SkipNext" class="md-fab md-primary">
            <md-icon >skip_next</md-icon>
          </md-button>
        </div>
        <div class="md-layout-item">
          <md-button v-on:click="Shuffle"class="md-fab md-primary">
            <md-icon >shuffle</md-icon>
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
      PlayDisplay: false,
      LetChoose: false,
      barId: 1,
      playlistName: 'European hits',
      playlists: [ { id: 1, name: 'bang bang' }, { id: 2, name: 'ding ding' }, { id: 0, name: '' } ],
      songs: [ { id: 1, name: 'Queen - Don\'t Stop Me Now' }, { id: 2, name: 'Survivor - Eye Of The Tiger' } ],
      selected: '',
    }
  },
  methods: {
    Shuffle: function()
    {
      console.log("Shuffle");
    },
    Play: function()
    {
      this.PlayDisplay = !this.PlayDisplay;
      console.log("Play" + this.PlayDisplay);
    },
    SkipPrevious: function()
    {
      console.log("SkipPrevious");
    },
    SkipNext: function()
    {
      console.log("SkipNext");
    },
    CreateNew: function()
    {
      console.log("CreateNew");
    },
    ArrowDown: function(i)
    {
      console.log("ArrowDown"+i);
    },
    ArrowUp: function(i)
    {
      console.log("ArrowUp"+i);
    },
    Blocked: function(i)
    {
      console.log("blocked"+i);
    },
    Remove: function()
    {
      console.log("removed");
    },
    LoadPlaylists: async function () {
     /* await axios.get('https://localhost:44341/api/playlist/getbarplaylists/' + this.barId)
        .then(response => (this.playlists = response.data))*/
    },
    LoadPlaylistSongs: async function () {
      /*await axios.get('https://localhost:44341/api/playlist/getplaylistsongs/' + this.playlist.id)
        .then(response => (this.songs = response.data))*/
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
