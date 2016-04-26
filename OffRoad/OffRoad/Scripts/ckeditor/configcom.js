/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    config.language = 'fr';
    config.toolbar =
	[
		{ name: 'clipboard', items: ['Cut', 'Copy', 'Paste', '-', 'Undo', 'Redo'] },
		{ name: 'insert', items: ['Smiley', 'SpecialChar'] },
		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
		{ name: 'colors', items: ['TextColor', 'BGColor'] },
		{ name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] }
	];
};