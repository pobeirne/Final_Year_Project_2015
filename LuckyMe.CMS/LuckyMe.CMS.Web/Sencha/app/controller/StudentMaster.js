Ext.define('School.controller.StudentMaster', {
    extend: 'Ext.app.Controller',
    models: ['School.model.Student'],
    views: ['School.view.StudentMaster'],

    refs: [{
        ref: 'studentMasterForm',
        selector: 'viewport > StudentMaster'
    }],

    init: function () {
        this.control({

            'viewport > StudentMaster button[itemId=btnCreate]': {
                click: this.onCreateClick
            },
            'viewport > StudentMaster button[itemId=btnLoad]': {
                click: this.onLoadClick
            },
            'viewport > StudentMaster button[itemId=btnUpdate]': {
                click: this.onUpdateClick
            },
            'viewport > StudentMaster button[itemId=btnDelete]': {
                click: this.onDeleteClick
            },
            'viewport > StudentMaster button[itemId=btnReset]': {
                click: this.onResetClick
            },
            'viewport > StudentMaster button[itemId=btnClear]': {
                click: this.onClearClick
            }
        }
            );
    },

    onUpdateClick: function () {

        var stdMaster = this.getStudentMasterForm();

        if (!stdMaster.isDirty()) {
            Ext.Msg.alert('Status', 'No pending changes to save.');
            return;
        }
        else if (!stdMaster.isValid()) {
            Ext.Msg.alert('Status', 'Invalid data.');
            return;
        }

        var studentModel = Ext.create('School.model.Student');

        studentModel.set(stdMaster.getValues());

        var mask = new Ext.LoadMask(stdMaster, { msg: "Updating..." });
        mask.show();

        studentModel.save({
            scope: this, //controller would be accessible inside load
            success: function (record, operation) {
                try {
                    var resp = Ext.decode(operation.response.responseText);
                    if (resp.success) {
                        //set the root in proxy to indicate where the student data in the response
                        var student = Ext.create('School.model.Student');
                        student.set(resp.data[0]);
                        this.getStudentMasterForm().loadRecord(student);

                        Ext.Msg.alert('Status', 'Student updated successfully.');
                    }
                    else
                        Ext.Msg.alert('Status', resp.message);
                }
                catch (ex) {
                    Ext.Msg.alert('Status', 'Invalid data in server response: ' + ex.Message);

                }
                mask.hide();
            },
            failure: function (record, operation) {
                Ext.Msg.alert('Save Failed', operation.response.responseText);
                try {
                    var resp = Ext.decode(operation.response.responseText);
                    Ext.Msg.alert('Status', resp.message);

                }
                catch (ex) {
                    Ext.Msg.alert('Status', 'Not a valid data.' + ex.Message);
                }
                mask.hide();
            }
        });

    },

    onLoadClick: function () {

        var existingStudent = Ext.ModelMgr.getModel('School.model.Student');
        var stdMaster = this.getStudentMasterForm();
        var mask = new Ext.LoadMask(stdMaster, { msg: "Loading..." });
        mask.show();

        existingStudent.load(1, {
            scope: this, //controller would be accessible inside load
            failure: function (record, operation) {
                mask.hide();
                Ext.Msg.alert('Status', 'Service request faild.');
            },
            success: function (record, operation) { //do something if the load succeeded
                //if service response object has same properties and heirarchy as model object then you can model.loadRecord(record)
                //otherwise you have to decode operation.response.responseText, create model object and populate valudes of decoded response into model and then use model.loadRecord()
                try {
                    //                    var resp = Ext.decode(operation.response.responseText);
                    //                    if (resp.success) {
                    //                        //set the root in proxy to indicate where the student data in the response
                    //                        var student = Ext.create('School.model.Student');
                    //                        student.set(resp.data[0]);

                    //                        this.getStudentMasterForm().loadRecord(student);
                    //                        Ext.Msg.alert('Status', 'Records loaded successfully.');
                    //                    }
                    //                    else
                    //                        Ext.Msg.alert('Status', resp.message);

                    //following also valid
                    this.getStudentMasterForm().loadRecord(record);
                    Ext.Msg.alert('Status', 'Records loaded successfully.');

                }
                catch (ex) {
                    Ext.Msg.alert('Status', 'Invalid data in server response: ' + ex.Message);

                }
                mask.hide();
            }
        });

    },

    onCreateClick: function () {
        var stdMaster = this.getStudentMasterForm();

        if (!stdMaster.isDirty()) {
            Ext.Msg.alert('Status', 'No new data to create.');
            return;
        }
        else if (!stdMaster.isValid()) {
            Ext.Msg.alert('Status', 'Invalid data.');
            return;
        }

        var studentModel = Ext.create('School.model.Student');

        studentModel.set(stdMaster.getValues());

        var mask = new Ext.LoadMask(stdMaster, { msg: "Saving..." });
        mask.show();

        studentModel.save({
            scope: this, //controller would be accessible inside load
            success: function (record, operation) { //do something if the load succeeded
                //if service response object has same properties and heirarchy as model object then you can model.loadRecord(record)
                //otherwise you have to decode operation.response.responseText, create model object and populate valudes of decoded response into model and then use model.loadRecord()
                try {
                    //                    var resp = Ext.decode(operation.response.responseText);
                    //                    if (resp.success) {
                    //                        //set the root in proxy to indicate where the student data in the response
                    //                        var student = Ext.create('School.model.Student');
                    //                        student.set(resp.data[0]);
                    //                        this.getStudentMasterForm().loadRecord(student);
                    //                          Ext.Msg.alert('Status', resp.message);
                    //                    }
                    //                    else
                    //                        Ext.Msg.alert('Status', resp.message);
                    //or
                    this.getStudentMasterForm().loadRecord(record);
                    Ext.Msg.alert('Status', 'Student Saved Successfully!');


                }
                catch (ex) {
                    Ext.Msg.alert('Status', 'Invalid data in server response: ' + ex.Message);
                }
                mask.hide();
            },
            failure: function (record, operation) {
                mask.hide();
                Ext.Msg.alert('Status', 'Service request faild.');
            }

        });

    },

    onDeleteClick: function () {

        var stdMaster = this.getStudentMasterForm();

        if (!stdMaster.getValues(false, false, false, true).Id) {
            Ext.Msg.alert('Status', 'Invalid or no data.');
            return;
        }

        var studentModel = stdMaster.getRecord();
        var mask = new Ext.LoadMask(stdMaster, { msg: "Deleting..." });
        mask.show();

        studentModel.destroy({
            scope: this, //controller would be accessible inside load
            failure: function (record, operation) {
                mask.hide();
                Ext.Msg.alert('Status', 'Service request faild.');
            },
            success: function (record, operation) {
                try {
                    var resp = Ext.decode(operation.response.responseText);
                    if (resp.success) { // check if delete succeded at server side
                        this.getStudentMasterForm().clearForm();
                    }

                    Ext.Msg.alert('Status', resp.message);
                }
                catch (ex) {
                    Ext.Msg.alert('Status', 'Exception: ' + ex.Message);
                }
                mask.hide();
            }
        });
    },
    onResetClick: function () {
        this.getStudentMasterForm().getForm().reset();
    },
    onClearClick: function () {
        this.getStudentMasterForm().clearForm();
    }
});