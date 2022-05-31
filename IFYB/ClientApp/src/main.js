import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import { createI18n } from 'vue-i18n'
import { messages } from './utils/i18nMessages'

const i18n = createI18n({
  locale: 'en',
  messages
})

createApp(App).use(store).use(router).use(i18n).mount('#app')
