<template>
	<editor
		:disabled=true
		:init= "{ }"
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
  }
}
</script>

<style scoped>
  .tox-editor-header {
    display: none!important;
  }
</style>
