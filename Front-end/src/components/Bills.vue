<template>
  <div class='center'>
    <md-card class='box'>
      <md-button class="md-raised md-primary margin" v-on:click="Create" >Kurti naują</md-button>
        <md-list style="width: 100%;">
          <md-divider class="md-inset"></md-divider>
          <div v-for="bill in bills" v-bind:key="HotReload">
            <md-list-item style="width: 100%;">
              <bill-Card :object="bill" @Deleted="onClickChild"></bill-Card>
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
import bill from '@/components/Event'
import Vue from 'vue'
import router from '../router/index.js'
import axios from 'axios'
var self = this
Vue.use(VueMaterial)
Vue.component('bill-Card', {
  props: ['object'],
  template: `
  <div style='width: 100%'>
  Data: {{ object.receiptDate.split('T')[0] }} {{ object.receiptDate.split('T')[1] }} Staliuko nr.: {{ object.tableNo }} Suma: {{ object.payAmount }}
    <md-button class='md-raised md-primary align-right' style="padding-top: 0.1%;">Redaguoti</md-button>
    <md-button class='md-raised md-primary align-right' style="padding-top: 0.1%;" >Šalinti</md-button>
  </div>`
})

export default {
  name: 'App',
  data () {
    return {
      bills: [],
      barId: 1
    }
  },
  methods: {
    onClickChild () {
      this.bills = []
      this.Load()
    },
    Create: function (bill) {
      // router.push({ name: 'bill', params: { new: 1 } })
    },
    Load: async function () {
      await axios.get('https://localhost:44341/api/Bill/GetBarBills/' + this.barId)
        .then(response => (this.bills = response.data))

      console.log(this.bills)
    }
  },

  async beforeMount () {
    // fetch data from api
    await this.Load()
  },
  components: {
    bill
  }
}
</script>
