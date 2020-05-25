<template>
  <div>
    <md-card>
      <div class="md-layout md-gutter">
        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-field md-clearable>
            <label>Pavadinimas</label>
            <md-input disabled v-model="selected.name"></md-input>
          </md-field>
        </div>

        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-field>
            <label for="playlists">Grojaraščių sąrašas</label>
            <md-select v-model="selected" md-dense>
                <md-option v-for="pl in playlists" :value="pl" :key="pl.id"> {{ pl.name }} </md-option>
            </md-select>
          </md-field>
        </div>

        <div class="md-layout-item" style="display: flex;align-items: center;">
          <md-field>
            <label for="playlists">Barų sąrašas</label>
            <md-select v-model="selectedBar" md-dense>
                <md-option v-for="bar in bars" :value="bar" :key="bar.id"> {{ bar.name }} </md-option>
            </md-select>
          </md-field>
        </div>
     </div>
      <div>
        <md-divider></md-divider>
        <md-list>
          <div v-for="s in songs" v-bind:key="s.rowid">
          <md-list-item  style="width: 100%;">
              <span class="md-list-item-text">{{s.name}}</span>            
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
      playlistName: '',
      playlists: [{ id: 0, name: '' } ],
      songs: [],
      selected: '',
      updateRefresh: 0,
      bars: [{ id: 0, name: '' } ],
      selectedBar: 0
    }
  },
  watch: {
    selected: function (event)
    {
      console.log(this.selected)
      this.LoadPlaylistSongs()
    },
    selectedBar: function (event)
    {
      console.log(this.selectedBar)
      this.LoadPlaylists()
    },
  },
  methods: {
    CreateNew: function()
    {
      router.push({ name: 'AddSong', params: { id: this.selected.id } })
      console.log("CreateNew");
    },
    LoadBars: async function () {
    var me = this;
      await axios.get('https://localhost:44341/api/bar/GetAllBars/').then(function(response){
        me.bars = response.data;
        me.LoadPlaylists();
      });
    },
      //GetAllBars
    LoadPlaylists: async function () {
      console.log('https://localhost:44341/api/playlist/GetBarPlaylists/' + this.selectedBar.barID);
      await axios.get('https://localhost:44341/api/playlist/GetBarPlaylists/' + this.selectedBar.barID)
        .then(response => (this.playlists = response.data))

      this.playlists = [...this.playlists, { id: 0, name: '' }]
    },
    LoadPlaylistSongs: async function () {
      var me = this;
      await axios.get('https://localhost:44341/api/playlist/getplaylistsongs/' + this.selected.id)
        .then(function(response){
          me.songs = response.data;
          console.log(response.data.length);
          for(var index = 0; index < response.data.length; index++)
          {
            me.songs[index].rowid = index;
          }
        })          
    }
  },
  async mounted () {
    // fetch data from api
    await this.LoadBars();
    console.log("hi");
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
