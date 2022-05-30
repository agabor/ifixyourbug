<template>
	<editor
		:api-key="key"
		:disabled=true
		:init= "{  }"
		:initial-value="value"
		:inline=true
		tag-name="div"
	/>
</template>

<script>
import Editor from '@tinymce/tinymce-vue';

export default {
  name: 'TextWiewer',
  components: { Editor },
  props: {
    value: String
  },
  setup() {
		const key = process.env.VUE_APP_TINY_API_KEY;
    return { key }
  }
}
</script>

<style scoped>
  .tox-editor-header {
    display: none!important;
  }
</style>
