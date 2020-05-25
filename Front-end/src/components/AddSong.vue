<template>
  <div>
    Paieška: <input type="text" v-model="search"/>   
    <div v-for="(song,i) in filteredSongs">
    <span v-on:click="AddSong(song.id)" >{{i+1}}. {{song.name}}</span>
    </div>
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
      playlistID: this.$route.params.id,
      selected: '',
      search: '',
      songs: [],
      customers: [
          { id: '1', name: 'Jhon Snow', profile_pic: 'https://i.stack.imgur.com/CE5lz.png'},
          { id: '2', name: 'Deanerys Targarian', profile_pic: 'https://i.stack.imgur.com/CE5lz.png'},
          { id: '3', name: 'Jaime Lanister', profile_pic: 'https://i.stack.imgur.com/CE5lz.png'},
          { id: '4', name: 'Tyron Lanister', profile_pic: 'https://i.stack.imgur.com/CE5lz.png'}
        ]};
    },
    async mounted () {
    // fetch data from api
    await this.Load();
    console.log("hi");
    },
    methods: {
        AddSong: async function(id)
        {
            await axios.post('https://localhost:44341/api/playlist/' + id + '/AddToPlaylist/' + this.playlistID)
            .then(response => console.log(response.data))
            //localhost/api/playlist/dainosid/AddToPlaylist/playlistoId
            router.push({ name: 'Playlist'})

        },
        Load: async function () {
        var me = this;
            await axios.get('https://localhost:44341/api/playlist/GetSongs/')
            .then(function(response){
            for(var i = 0; i < response.data.length; i++)
            {
                var tempobj = {};
                me.songs.push(response.data[i]);
            }
            me.songs.splice(10);
        })   

        }
    },
    //GetSongs
    computed:
    {
        filteredSongs:function()
        {
            var self=this;
            return this.songs.filter(function(cust){return cust.name.toLowerCase().indexOf(self.search.toLowerCase())>=0;});
        }
    }
  };  

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
