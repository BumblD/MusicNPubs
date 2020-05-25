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
                <md-option v-for="pl in playlists" :value="pl" :key="pl.id" v-on:click="SelectPlaylist(pl)"> {{ pl.name }} </md-option>
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
          <div v-for="s in songs" v-bind:key="s.rowid">
          <md-list-item  style="width: 100%;">
              <span class="md-list-item-text">{{s.name}}</span>
              <md-button v-on:click="ArrowUp(s.rowid)" class="md-icon-button md-dense md-primary">
                <md-icon >arrow_upward</md-icon>
              </md-button>
              <md-button v-on:click="ArrowDown(s.rowid)" class="md-icon-button md-dense md-accent">
                <md-icon >arrow_downward</md-icon>
              </md-button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <md-button v-on:click="Blocked(s.id)" class="md-icon-button md-dense md-accent">
                <md-icon >block</md-icon>
              </md-button>
              <md-button v-on:click="Remove(s.id)" class='md-raised md-primary align-right button-down' >Šalinti</md-button>
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
      playlistName: '',
      playlists: [{ id: 0, name: '' } ],
      songs: [],
      selected: '',
      updateRefresh: 0
    }
  },
  watch: {
    selected: function (event)
    {
      console.log(this.selected)
      this.LoadPlaylistSongs()
    }
  },
  methods: {
    Shuffle: function()
    {
      for (var i = this.songs.length - 1; i > 0; i--) {
        var j = Math.floor(Math.random() * (i + 1));
        var temp = this.songs[i].rowid;
        this.songs[i].rowid = this.songs[j].rowid;
        this.songs[j].rowid = temp;
      }
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
      router.push({ name: 'AddSong', params: { id: this.selected.id } })
      console.log("CreateNew");
    },
    ArrowDown: function(currentOrder)
    {
      function compare( a, b )
      {
        if ( a.rowid < b.rowid ){
          return -1;
        }
        if ( a.rowid > b.rowid ){
          return 1;
        }
        return 0;
      }
      var nextindex = this.songs.findIndex(x => x.rowid == currentOrder)
      var currentindex = this.songs.findIndex(x => x.rowid == currentOrder+1)
      var idprevious = this.songs[nextindex].rowid;
      var idnext = this.songs[currentindex].rowid;
      this.songs[nextindex].rowid = idnext
      this.songs[currentindex].rowid = idprevious;
      this.songs.sort(compare);
    },
    ArrowUp: function(currentOrder)
    {
      function compare( a, b )
      {
        if ( a.rowid < b.rowid ){
          return -1;
        }
        if ( a.rowid > b.rowid ){
          return 1;
        }
        return 0;
      }
      var currentindex = this.songs.findIndex(x => x.rowid == currentOrder)
      var nextindex = this.songs.findIndex(x => x.rowid == currentOrder-1)
      var idprevious = this.songs[currentindex].rowid;
      var idnext = this.songs[nextindex].rowid;
      this.songs[currentindex].rowid = idnext
      this.songs[nextindex].rowid = idprevious;
      this.songs.sort(compare);
    },
    Blocked: async function(i)
    {
      if (confirm('Ar tikrai norite blokuoti šią dainą?')) {
      await axios.post('https://localhost:44341/api/playlist/' + this.selected.id + '/BlockSong/' + i)
        .then(response => console.log(response.data))
      this.LoadPlaylistSongs()
      }
    },
    Remove: async function(songid)
    {
      if (confirm('Ar tikrai norite pašalinti šią dainą?')) {
      //RemoveToPlaylist
      var me = this;
      console.log(songid);
      await axios.post('https://localhost:44341/api/playlist/' + songid + '/RemoveToPlaylist/' + this.selected.id).then(function(){
        console.log("removed");
        me.LoadPlaylistSongs()
      })    
      }
    },
    LoadPlaylists: async function () {
      await axios.get('https://localhost:44341/api/playlist/GetBarPlaylists/' + this.barId)
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
