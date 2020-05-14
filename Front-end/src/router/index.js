import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import EventList from '@/components/EventList'
import Event from '@/components/Event'
import MapPage from '@/components/Map.vue'
import BarDetails from '@/components/BarDetails.vue'
import Reviews from '@/components/Reviews.vue'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/EventList',
      name: 'EventList',
      component: EventList
    },
    {
      path: '/Event',
      name: 'Event',
      component: Event
    },
    {
      path: '/Map',
      name: 'Map',
      component: MapPage
    },
    {
      path: '/BarDetails',
      name: 'BarDetails',
      component: BarDetails
    },
    {
      path: '/Reviews',
      name: 'Reviews',
      component: Reviews
    },
  ]
})
