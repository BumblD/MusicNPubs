<template>
  <md-card>
    <div>
      <md-field>
        <label>Pavadinimas</label>
        <md-input v-model="Event.name"></md-input>
      </md-field>
      <md-datepicker v-model="Event.date">
        <label>Data, laikas</label>
      </md-datepicker>
      <md-field>
        <label>Įėjimo kaina</label>
        <md-input v-model="Event.price"></md-input>
      </md-field>
      <md-field>
        <label>Plakatas</label>
        <md-file v-model="Event.poster" />
      </md-field>
      <md-field>
        <label>Aprašymas</label>
        <md-textarea v-model="Event.description"></md-textarea>
      </md-field>
      <md-button
        class="md-raised md-primary align-right center-vertical"
        v-on:click="Submit"
      >Pateikti</md-button>
      <md-button
        class="md-raised md-secondary align-right center-vertical"
        v-on:click="Cancel"
      >Atšaukti</md-button>
    </div>
  </md-card>
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
  methods: {
    Submit: async function () {
      if (this.Create === 1) {
        console.log(this.Event)
        await axios
          .post('https://localhost:44341/api/Events/CreateEvent', {
            Name: this.Event.name,
            Date: this.Event.date,
            Price: Number(this.Event.price),
            Description: this.Event.description,
            Poster: this.Event.poster,
            BarId: 1
          })
          .then(function (response) {
            alert('Duomenys apie renginį sukurti sėkmingai!')
          })
          // eslint-disable-next-line handle-callback-err
          .catch(function (error) {
            alert('Klaida! Nepavyko sukurti duomenų!')
          })
        router.push({ name: 'EventList'})
      } else {
        axios
          .post('https://localhost:44341/api/Events/UpdateEvent', {
            EventId: Number(this.Event.eventId),
            Name: this.Event.name,
            Date: this.Event.date,
            Price: Number(this.Event.price),
            Description: this.Event.description,
            Poster: this.Event.poster,
            BarId: Number(this.Event.barId)
          })
          .then(function (response) {
            alert('Duomenys apie renginį atnaujinti sėkmingai!')
          })
        // eslint-disable-next-line handle-callback-err
          .catch(function (error) {
            alert('Klaida! Nepavyko atnaujinti duomenų!')
          })
        router.push({ name: 'EventList'})
      }
    },
    Cancel: function () {
      router.push('EventList')
    }
  },
  beforeMount () {
    if(this.Event != null)
    {
      this.Event.date =  this.Event.date.split('T')[0];
    }
    if (this.Create === 1) {
      this.Event = {
        date: '2020-01-01'
      }
    }
  },
  data: function () {
    return {
      Event: this.$route.params.event,
      Create: this.$route.params.new
    }
  }
}
</script>
<style scoped>
.md-card {
  width: 30%;
  display: inline-block;
  vertical-align: top;
  padding: 2%;
}
</style>
