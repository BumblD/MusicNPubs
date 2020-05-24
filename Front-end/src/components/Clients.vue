<template>
  <div class='center'>
    <md-card class='box'>
        <md-list style="width: 100%;">
          <div v-for="client in clients" v-bind:key="HotReload">
            <md-list-item style="width: 100%;">
              <client-Card :object="client" @Deleted="onClickChild"></client-Card>
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
import client from '@/components/Event'
import Vue from 'vue'
import router from '../router/index.js'
import axios from 'axios'
var self = this
Vue.use(VueMaterial)
Vue.component('client-Card', {
  props: ['object'],
  template: `
  <div style='width: 100%'>
  {{ object.username }}
    <md-button class='md-raised md-primary align-right' style="padding-top: 0.1%;" v-if="object.isBlocked === true">Atblokuoti</md-button>
    <md-button class='md-raised md-accent align-right' style="padding-top: 0.1%;" v-else>Blokuoti</md-button>
  </div>`
})

export default {
  name: 'App',
  data () {
    return {
      clients: []
    }
  },
  methods: {
    onClickChild () {
      this.clients = []
      this.Load()
    },
    Load: async function () {
      await axios.get('https://localhost:44341/api/system/getclients')
        .then(response => (this.clients = response.data))
    }
  },

  async beforeMount () {
    // fetch data from api
    await this.Load()
  },
  components: {
    client
  }
}
</script>

