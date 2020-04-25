<template>
  <div id="App">
    Renginio ID = {{ this.$route.params.ID}}
    <div class="inputs">
      <md-field >
        <label>Pavadinimas</label>
        <md-input v-model="Event.Name"></md-input>
      </md-field>
      <md-field>
        <label>Data, laikas</label>
        <md-input v-model="Event.Date"></md-input>
      </md-field>
      <md-field>
        <label>Iejimo kaina</label>
        <md-input v-model="Event.EntranceFee"></md-input>
      </md-field>
      <md-field>
        <label>Cover</label>
        <md-file v-model="Event.Coverurl" />
      </md-field>
      <md-field>
        <label>Aprasymas</label>
        <md-textarea v-model="Event.Description"></md-textarea>
      </md-field>
    </div>
  </div>
</template>
<script>
import Vue from 'vue'
import VueMaterial from 'vue-material'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
Vue.use(VueMaterial)

export default {
  name: 'App',
  data: function () {
    return {
      Event: {},
      Events: [
        {
          Name: 'Renginys1',
          Date: '2020-04-22',
          EntranceFee: 20,
          Coverurl: '',
          Description: 'fun',
          id: 1
        },
        {
          Name: 'Renginys2',
          Date: '2020-04-22',
          EntranceFee: 20,
          Coverurl: '',
          Description: 'kinda fun',
          id: 2
        },
        {
          Name: 'Renginys3',
          Date: '2020-04-22',
          EntranceFee: 20,
          Coverurl: '',
          Description: 'semi fun',
          id: 3
        },
        {
          Name: 'Renginys4',
          Date: '2020-04-22',
          EntranceFee: 20,
          Coverurl: '',
          Description: 'no fun',
          id: 4
        }
      ]
    }
  },
  mounted () {
    for (var i = 0; i < this.Events.length; i++) {
      if (this.Events[i].id === this.$route.params.ID) {
        this.Event = this.Events[i]
      }
    }
  }
}
</script>
<style scoped>
.inputs {
  padding-left: 40%;
  padding-right: 40%;
}
</style>
