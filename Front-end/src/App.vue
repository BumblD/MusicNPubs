<template>
  <div id="app">
    <h1>Muzika ir barai</h1>
      <ul>
        <router-link to="/EventList">Renginiai</router-link>
        <router-link to="/Map">Žemėlapis</router-link>
        <router-link to="/">Grojaraštis</router-link>
        <router-link to="/">Lankytojai</router-link>
        <router-link to="/">Sąskaitos</router-link>
    </ul>
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
