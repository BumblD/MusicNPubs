<template>
  <div class='center'>
    <md-card class='box'>
      <md-button class="md-raised md-primary margin" v-on:click="Create" >Kurti naują</md-button>
        <md-list class='width'>
          <md-divider class="md-inset"></md-divider>
          <div v-for="event in events" v-bind:key="event.eventId">
            <md-list-item>
              <Event-Card :object="event"></Event-Card>
            </md-list-item>
            <md-divider class="md-inset"></md-divider>
          </div>
        </md-list>
      </md-card>
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
      router.push({ name: 'Event', params: { event: this.object } })
    },
    Remove: function () {
      if (confirm('Ar tikrai norite pašalinti šį įvykį?')) {
        axios.delete('https://localhost:44341/api/Events/DeleteEvent/' + this.object.eventId)
          .then(function (response) {
            console.log(alert('Renginys ištrintas!'))
          })
        // eslint-disable-next-line handle-callback-err
          .catch(function (error) {
            alert('Klaida! Nepavyko ištrinti renginio!')
          })
      }
    }
  },
  template: `
  <div class='width'>
  <h4>{{ object.name }}
    <md-button class='md-raised md-primary align-right center-vertical' v-on:click='Detailed' >Redaguoti</md-button>
    <md-button class='md-raised md-primary align-right center-vertical' v-on:click='Remove' >Šalinti</md-button>
  </h4>
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
    Create: function (event) {
      router.push({ name: 'Event', params: { new: 1 } })
    }
  },

  async mounted () {
    // fetch data from api
    await axios.get('https://localhost:44341/api/events/getallevents')
      .then(response => (this.events = response.data))
  },
  components: {
    Event
  }
}
</script>

<style>
.box {
  text-align: left;
}

.center {
  margin: auto;
  width: 50%;
  padding: 10px;
}

.center-vertical {
  top: 50%;
  -ms-transform: translateY(-50%);
  transform: translateY(-50%);
}

.border {
  border: 3px solid grey;
  padding-left: 1%;
  padding-right: 1%;
  margin: 1%;
}

.align-right {
  float: right;
  padding-left: 1%;
  padding-right: 1%;
}

.width {
  width: 100%
}

.margin {
  margin: 2%;
}
</style>
