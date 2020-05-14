<template>
  <div id="app">
    Baro {{barID}} Atsiliepimai
    <div width="60%" right="20%">
        <div v-for="review in reviews">
            <md-list-item>
                {{review.description}}
            </md-list-item>
            <md-divider class="md-inset"></md-divider>
        </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'App',
  data: function () {
    return {
      barID: this.$route.params.barID,
      reviews: [
      {description: "sucks"},
      {description: "can confirm this one is bad"}]
    }
  },
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
