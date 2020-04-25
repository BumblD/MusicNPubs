<template>
  <div id="App">
    <button>Kurti nauja</button>
      <div v-for="event in Events" v-bind:key="event.id">
          <Event-Card :object="event"></Event-Card>
      </div>
    </div>
</template>

<script>
import Event from '@/components/Event'
import Vue from 'vue'
import router from '../router/index.js'
import axios from 'axios'

Vue.component('Event-Card', {

  props: ['object'],
  /* data: function () {
    return {
      count: 0
    }
  }, */
  methods: {
    Detailed: function () {
      var ID = this.object.id
      router.push({ name: 'Event', params: { ID } })
    }
  },
  template: `
  <div>
  <h4>{{ object.name }}
  <button v-on:click="Detailed" >Redaguoti</button> <button>Salinti</button>
  </h4>
  </div>`
})

export default {
  name: 'App',
  data: function () {
    // fetch data from api
    axios.get('https://localhost:44341/api/events/getevents')
      .then(response => (console.log(response.data)))

    return {
      Events: [{
        name: 'Renginys1',
        id: 1
      }, {
        name: 'Renginys2',
        id: 2
      }, {
        name: 'Renginys3',
        id: 3
      }, {
        name: 'Renginys4',
        id: 4
      }]
    }
  },
  components: {
    Event
  }
}
</script>
