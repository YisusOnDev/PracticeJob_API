﻿using AutoMapper;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Email;
using PracticeJob.Core.Security;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.BL.Implementations
{
    public class CompanyBL : ICompanyBL
    {
        public ICompanyRepository CompanyRepository { get; set; }
        public IPasswordGenerator PwdGenerator { get; set; }
        public IMapper Mapper { get; set; }
        
        public IEmailSender EmailSender { get; set; }

        public CompanyBL(ICompanyRepository CompanyRepository, IPasswordGenerator PwdGenerator, IMapper Mapper, IEmailSender EmailSender)
        {
            this.CompanyRepository = CompanyRepository;
            this.PwdGenerator = PwdGenerator;
            this.Mapper = Mapper;
            this.EmailSender = EmailSender;
        }
        public CompanyDTO Login(AuthDTO authDTO)
        {
            authDTO.Password = PwdGenerator.Hash(authDTO.Password);
            var loginData = Mapper.Map<AuthDTO, Company>(authDTO);
            var company = Mapper.Map<Company, CompanyDTO>(CompanyRepository.Login(loginData));
            return company;
        }
        public CompanyDTO Get(int companyId)
        {
            var company = Mapper.Map<Company, CompanyDTO>(CompanyRepository.Get(companyId));
            return company;
        }

        public CompanyDTO Create(AuthDTO authDTO)
        {
            authDTO.Password = PwdGenerator.Hash(authDTO.Password);

            var company = Mapper.Map<AuthDTO, Company>(authDTO);

            if (!CompanyRepository.Exists(company))
            {
                var c = Mapper.Map<Company, CompanyDTO>(CompanyRepository.Create(company));
                return c;
            }

            return null;
        }

        public CompanyDTO Update(CompanyDTO companyDTO)
        {
            var company = Mapper.Map<CompanyDTO, Company>(companyDTO);
            var updCompany = Mapper.Map<Company, CompanyDTO>(CompanyRepository.Update(company));
            return updCompany;
        }

        public void ConfirmEmailSend(CompanyDTO companyDTO)
        {
            var confirmCode = CompanyRepository.Generate2FACode(companyDTO.Email);
            EmailSender.SendConfirmationMail(companyDTO.Email, confirmCode);
        }

        public CompanyDTO ValidateEmail(CompanyDTO companyDTO, string code)
        {
            var company = Mapper.Map<CompanyDTO, Company>(companyDTO);
            return Mapper.Map<Company, CompanyDTO>(CompanyRepository.ValidateEmail(company, code));
        }
        public bool ResetPasswordSend(string email)
        {
            var accountExists = CompanyRepository.EmailRegistered(email);
            if (accountExists)
            {
                var confirmCode = CompanyRepository.Generate2FACode(email);
                EmailSender.SendPasswordReset(email, confirmCode);
                return true;
            }
            return false;
        }

        public bool UpdatePassword(PasswordResetDTO passwordReset)
        {
            passwordReset.Password = PwdGenerator.Hash(passwordReset.Password);
            var newPasswordReset = Mapper.Map<PasswordResetDTO, PasswordReset>(passwordReset);
            var passwordReseted = CompanyRepository.UpdatePassword(newPasswordReset);
            return passwordReseted;
        }

        public bool ContactStudent(ContactMailDTO contactMailDTO)
        {
            ContactMail contactMail = Mapper.Map<ContactMailDTO, ContactMail>(contactMailDTO);
            EmailSender.SendCompanyContact(contactMail);
            return true;
        }

        public void SetProfileImage(int companyId, string fileName)
        {
            CompanyRepository.SetProfileImage(companyId, fileName);
        }
    }
}
