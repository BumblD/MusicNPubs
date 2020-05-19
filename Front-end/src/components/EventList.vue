<template>
  <div class='center'>
    <md-card class='box'>
      <md-button class="md-raised md-primary margin" v-on:click="Create" >Kurti naują</md-button>
        <md-list class='width'>
          <md-divider class="md-inset"></md-divider>
          <div v-for="event in events" v-bind:key="HotReload">
            <md-list-item>
              <Event-Card :object="event" @Deleted="onClickChild"></Event-Card>
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
var self = this
Vue.use(VueMaterial)
Vue.component('Event-Card', {
  props: ['object'],
  methods: {
    Detailed: function () {
      router.push({ name: 'Event', params: { event: this.object } })
    },
    Remove: async function () {
      if (confirm('Ar tikrai norite pašalinti šį įvykį?')) {
        await axios.delete('https://localhost:44341/api/Events/DeleteEvent/' + this.object.eventId)
          .then(function (response) {
            console.log(alert('Renginys ištrintas!'))
          })
        // eslint-disable-next-line handle-callback-err
          .catch(function (error) {
            alert('Klaida! Nepavyko ištrinti renginio!')
          })
        this.$emit('Deleted')
      }
    }
  },
  template: `
  <div style='width: 100%'>
  {{ object.name }}
    <md-button class='md-raised md-primary align-right center-vertical' v-on:click='Detailed' >Redaguoti</md-button>
    <md-button class='md-raised md-primary align-right center-vertical' v-on:click='Remove' >Šalinti</md-button>

  </div>`
})

export default {
  name: 'App',
  data () {
    return {
      events: []
    }
  },
  methods: {
    onClickChild () {
      this.events = []
      this.Load()
    },
    Create: function (event) {
      router.push({ name: 'Event', params: { new: 1 } })
    },
    Load: async function () {
      await axios.get('https://localhost:44341/api/events/getallevents')
        .then(response => (this.events = response.data))
    }
  },

  async beforeMount () {
    // fetch data from api
    await this.Load()
  },
  components: {
    Event
  }
}
</script>
