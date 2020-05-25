<template>
  <div id="app">
    Baro {{barID}} Atsiliepimai
    <div width="60%" right="20%">
        <div v-for="review in reviews">
          Atsiliepimas: {{review.review}} Data: {{review.date.split('T')[0]}}  Ivertinimas: {{review.grade}}
          <md-divider class="md-inset"></md-divider>
        </div>
    </div>
    <md-button v-on:click="CreateNew" class="md-fab md-mini md-primary center-horizontal">
      <md-icon >add</md-icon>
    </md-button>
  </div>
</template>

<script>
import Vue from 'vue'
import router from '../router/index.js'
import axios from 'axios'
export default {
  name: 'App',
  data: function () {
    return {
      barID: this.$route.params.barID,
      reviews: []
    }
  },
  async beforeMount () {
    // fetch data from api
    await this.Load();
  },
  methods: {
    CreateNew: function(){
      console.log("createNew");
    },
    Load: async function () {
      var me = this;
        await axios.get('https://localhost:44341/api/Review/GetAllReviews/'+this.barID)
      .then(function(response){
          for(var i = 0; i < response.data.length; i++)
          {
            var tempobj = {};
            me.reviews.push(response.data[i]);
          }
      })   

    }
  }
}
</script>
<style scoped>
.div {
  width: 30%;
  display: inline-block;
  vertical-align: top;
  padding: 2%;
}
</style>
