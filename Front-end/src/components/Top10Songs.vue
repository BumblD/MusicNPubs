<template>
  <div class='center'>
    <md-card class='box'>
        <md-list style="width: 100%;">
          <md-divider class="md-inset"></md-divider>
          <div v-for="song in songs" v-bind:key="HotReload">
            <md-list-item style="width: 100%;">
            Pavadinimas: {{song.name}}
            <br>
            Autorius: {{song.author}}
            <br>
            Klausymų kiekis: {{song.listeningCount}}
            <br>
            </md-list-item>
            <md-divider class="md-inset"></md-divider>
          </div>
        </md-list>
      </md-card>
    </div>
</template>

<script>
import '../Styles/EventList.css'
import VueMaterial from 'vue-material'
import Event from '@/components/Event'
import Vue from 'vue'
import router from '../router/index.js'
import axios from 'axios'
Vue.use(VueMaterial)

export default {
  name: 'App',
  data () {
    return {
      songs: []
    }
  },
  methods: {
    Load: async function () {
      await axios.get('https://localhost:44341/api/playlist/GetTop10/')
        .then(response => (this.songs = response.data))
    }
  },
  async beforeMount () {
    // fetch data from api
    await this.Load()
  }
}
</script>