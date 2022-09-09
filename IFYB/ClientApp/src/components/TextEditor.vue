<template>
	<editor class="border-radius-lg" :modelValue="modelValue" @update:modelValue="$emit('update:modelValue', $event)" :placeholder="placeholder" :init="{
		height: 300,
		menubar: false,
		images_upload_url: '/api/image',
		images_upload_base_path: '/',
		plugins: [
			'advlist', 'autolink',
			'lists', 'link', 'image', 'charmap', 'preview', 'anchor', 'searchreplace', 'visualblocks',
			'fullscreen', 'insertdatetime', 'media', 'table', 'help'
		],
		toolbar:
			'undo redo | image | casechange blocks | bold italic backcolor | \
        alignleft aligncenter alignright alignjustify | \
        bullist numlst checklist outdent indent | removeformat | a11ycheck code table help'
	}" />
</template>

<script>
import Editor from '@tinymce/tinymce-vue';

export default {
	name: 'TextEditor',
	components: { Editor },
	props: {
		modelValue: String,
		placeholder: String
	},
	emits: ['update:modelValue']
}
</script>

<style>
.tox-statusbar {
	display: none !important;
}
</style>